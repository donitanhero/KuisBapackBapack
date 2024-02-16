using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class QuestionFetcher : MonoBehaviour
{
    [SerializeField] private List<LevelData> _levelData = new List<LevelData>();
    public struct userAttributes
    {

    }

    public struct appAttributes
    {

    }

    // RemoteConfigService.Instance.FetchConfigs() must be called with the attributes structs (empty or with custom attributes) to initiate the WebRequest.

    async void Awake()
    {
        StaticAction.GetLevelData += GetLevelData;
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
            Debug.Log("connected");
            await InitializeRemoteConfigAsync();
        }
        else
        {
            Debug.Log("not connected");
            return;
        }

        // Add a listener to apply settings when successfully retrieved:
        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;

        // Fetch configuration settings from the remote service:
        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());

      
    }

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

   

    // Create a function to set your variables to their keyed values:
    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        List<LevelData> levelData = JsonConverter.ReadFromJSON<List<LevelData>>(RemoteConfigService.Instance.appConfig.GetJson("Level_Data").Replace("\r\n", ""));
        foreach (var leveldata in levelData)
        {
            leveldata.questionData = JsonConverter.ReadFromJSON<List<QuestionData>>(RemoteConfigService.Instance.appConfig.GetJson(leveldata.LevelName).Replace("\r\n", ""));
            
            foreach(var data in leveldata.questionData)
            {
                if (data.Choice == null || data.Choice == string.Empty) break;
                data.ChoiceList = new List<string>();
                data.ChoiceList = data.Choice.Split(",").ToList();
            }
            _levelData.Add(leveldata);
        }
        StaticAction.IsQuestionFetched = true;
    }

    private void GetLevelData(Action<List<LevelData>> callback)
    {
        callback?.Invoke(_levelData);
    }
}

