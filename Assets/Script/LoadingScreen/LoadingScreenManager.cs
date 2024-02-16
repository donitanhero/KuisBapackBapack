using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    private const string CONTENT1 = "Do Not Show Again";
    private const string CONTENT2 = "Ok";

    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _loadingScreenGO;

    private float _taskCount;
    private float _taskComplete;
    private bool _isOldPlayer; //old or new player
    private const string PERCENTAGE = "%";
    private const float ONEHUNDRED = 100f;
    private bool _isLoaded;


    private void Awake()
    {
        StaticAction.OnSceneChange = OnSceneChange;
        _loadingScreenGO.SetActive(false);
    }


    private IEnumerator OnLoaded(int sceneIndex)
    {
        //yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncOperation.isDone)
        {
            UpdateUI(asyncOperation.progress);
            yield return null;
        }
        UpdateUI(asyncOperation.progress);
        yield return new WaitForSeconds(ConstVar.TWO);

        _loadingScreenGO.SetActive(false);


    }

    private void OnSceneChange(int sceneIndex)
    {
        _slider.value = ConstVar.ZERO;
        _loadingScreenGO.SetActive(true);

        StartCoroutine(OnLoaded(sceneIndex));
    }

    private void UpdateUI(float value)
    {
        _slider.value = value;
        //_txtPercentage.SetText(Mathf.Floor(value * ONEHUNDRED) + PERCENTAGE);
    }

}
