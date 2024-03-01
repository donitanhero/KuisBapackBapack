using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private LevelObj _prefab;
    [SerializeField] private Transform _parent;

    

    // Start is called before the first frame update
    void Start()
    {
        StaticAction.GetLevelData?.Invoke(SetUpLevelData);
        StaticAction.OnMusicPlay(ConstVar.SOUND_MAIN_MENU_MUSIC);
    }


    private void SetUpLevelData(List<LevelData> levelData)
    {

        for(int i=0; i< levelData.Count; i++)
        {
            LevelObj newLevelObj = Instantiate(_prefab, _parent);
            Debug.Log(i);
            Debug.Log(StaticPlayerPref.GetPlayerData()+1);
            Debug.Log("================================================");
            newLevelObj.SetUp(i, i <= (StaticPlayerPref.GetPlayerData()+1));
        }

    }
}
