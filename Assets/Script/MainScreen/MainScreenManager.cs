using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _openingScreen;
    [SerializeField] private Animator _animatorOpeningScreen;
    [SerializeField] private Button _btnStartGame;

    private float tempTime;

    private void Awake()
    {
        _openingScreen.gameObject.SetActive(true);
        _btnStartGame.onClick.AddListener(() => StaticAction.OnSceneChange?.Invoke(ConstVar.MAIN_MENU_SCENE));
        
    }

    // Start is called before the first frame update
    void Start()
    {
        tempTime = Time.time;
        StaticAction.OnMusicPlay(ConstVar.SOUND_MAIN_SCREEN_MUSIC);
        StartCoroutine(WaitForQuestionFetched());
    }

    private IEnumerator WaitForQuestionFetched()
    {
        yield return new WaitUntil(() => StaticAction.IsQuestionFetched);
        if(Time.time - tempTime < 2.5f) yield return new WaitForSeconds(2.5f);
        _animatorOpeningScreen.SetBool("QuestionFetched", true);
        
    }

}
