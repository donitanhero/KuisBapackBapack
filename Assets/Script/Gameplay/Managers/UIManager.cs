using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject[] _imgHeart;

    [Header("True Answer UI")]
    [SerializeField] private GameObject _pnlTrueAnswer;

    [Header("False Answer UI")]
    [SerializeField] private GameObject _pnlWrongAnswer;

    [Header("Panel Win")]
    [SerializeField] private GameObject _pnlWinGO;
    [SerializeField] private TextMeshProUGUI _txtLevelWin;
    [SerializeField] private Button _btnWinQuit;

    [Header("Panel Lose")]
    [SerializeField] private GameObject _pnlLoseGO;
    [SerializeField] private TextMeshProUGUI _txtLevelLose;
    [SerializeField] private Button _btnLoseQuit;
    [SerializeField] private Button _btnLoseRestart;

    private void Awake()
    {
        _btnLoseQuit.onClick.AddListener(Quit);
        _btnLoseRestart.onClick.AddListener(Restart);
        _btnWinQuit.onClick.AddListener(Quit);

        StaticAction.ReduceHeart += ReduceHeart;
        StaticAction.ResetHeart += ResetHeart;
        StaticAction.Win += Win;
        StaticAction.Lose += Lose;

        StaticAction.OnWrongAnswer += WrongAnswer;
        StaticAction.OnTrueAnswer += TrueAnswer;

    }

    private void OnDestroy() {
        StaticAction.ReduceHeart -= ReduceHeart;
        StaticAction.ResetHeart -= ResetHeart;
        StaticAction.Win -= Win;
        StaticAction.Lose -= Lose;

        StaticAction.OnWrongAnswer -= WrongAnswer;
        StaticAction.OnTrueAnswer -= TrueAnswer;
    }


    private void ReduceHeart(int currentHeart)
    {
        _imgHeart[currentHeart].SetActive(false);
    }

    private void ResetHeart()
    {
        foreach(var obj in _imgHeart){
            obj.SetActive(true);
        }
    }


    private void TrueAnswer(){
        _pnlTrueAnswer.SetActive(true);
    }

    private void WrongAnswer(){
        _pnlWrongAnswer.SetActive(true);
    }

    private void Win(string levelName){
        _txtLevelWin.SetText(levelName);
        _pnlWinGO.SetActive(true);
        StaticAction.OnSFXSoundPlay(ConstVar.SOUND_WIN_ROUND_SFX);
    }

    private void Lose(string levelName){
        _txtLevelLose.SetText(levelName);
        
        _pnlLoseGO.SetActive(true);
        StaticAction.OnSFXSoundPlay(ConstVar.SOUND_LOSE_ROUND_SFX);
    }


    private void Quit(){
        StaticAction.OnSceneChange?.Invoke(ConstVar.MAIN_MENU_SCENE);
    }

    private void Restart(){
        StaticAction.OnSceneChange?.Invoke(ConstVar.GAMEPLAY_SCENE);
    }

    private void NextLevel(){

    }
    
  
    

}
