using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManagers : MonoBehaviour
{
    private const string TXT_CLUE = "Clue: ";
    private const int _maxHealth = 3;

    [SerializeField] private List<QuestionData> _questionData;
    [Header("Prefab")]
    [SerializeField] private CharObj _charObjPrefab;
    [SerializeField] private Transform _charObjPrefabParent;
    
   

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _txtLevelName;
    [SerializeField] private TextMeshProUGUI _txtQuestion;
    [SerializeField] private TextMeshProUGUI _txtClue;

    [SerializeField] private TextMeshProUGUI _txtAnswer;


    [SerializeField] private Button _btnCheckAnswer;
    [SerializeField] private Button _btnDeleteAnswer;
    [SerializeField] private Button _btnHelp;
    




    private QuestionData _currentQuiz;
    private int _currentIndex;
    private List<CharObj> charObjList = new List<CharObj>();
    private string _currentAnswer;

    private int _health;

    private string _currentLevelName;
    


    private void Awake()
    {
        _questionData = new List<QuestionData>();
        _currentIndex = -1;
        _btnCheckAnswer.onClick.AddListener(CheckAnswer);
        _btnDeleteAnswer.onClick.AddListener(DeleteAnswer);
        _btnHelp.onClick.AddListener(Help);

        StaticAction.GiveAnswer += GiveAnswer;
        StaticAction.NextQuestion += NextQuestion;
        StaticAction.OnWrongAnswer += AnswerWrong;
        StaticAction.GetLevelData?.Invoke(SetQuestionData);
       

        _btnCheckAnswer.gameObject.SetActive(false);
        NextQuestion();
        
        
    }

    private void Start() {
        StaticAction.OnMusicPlay(ConstVar.SOUND_GAMEPLAY_MUSIC);
    }

    private void Update() {
        #if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.A)){
                StaticAction.OnTrueAnswer?.Invoke();
            }
        #endif
    }

    private void OnDestroy() {
        StaticAction.GiveAnswer -= GiveAnswer;
        StaticAction.NextQuestion -= NextQuestion;
        StaticAction.OnWrongAnswer -= AnswerWrong;
    }

    private void SetQuestionData(List<LevelData> levelData)
    {
        _questionData = levelData[PlayerData.CurrentLevelIndex].questionData;
        _currentLevelName = levelData[PlayerData.CurrentLevelIndex].LevelName;
        _txtLevelName.SetText(_currentLevelName);
    }

    private void Help()
    {
        SetClue(_currentQuiz.Help);
        StaticAction.OnSFXSoundPlay(ConstVar.SOUND_HINT_SFX);
    }

    private void ShowQuestion()
    {
        if (_currentQuiz != null)
        {
            _txtQuestion.SetText(_currentQuiz.Question);
            SetClue(_currentQuiz.Clue);
        }
    }

    private void SetClue(string clue)
    {
        _txtClue.SetText(TXT_CLUE + clue);
    }

    private void SetUpAnswer()
    {
        _currentAnswer = string.Empty;
        _txtAnswer.SetText(_currentAnswer);
        SetUpCharacterForAnswer();
    }

    private void GiveAnswer(string AnswerChar)
    {
        _currentAnswer += AnswerChar;
        _txtAnswer.SetText(_currentAnswer);
        _btnCheckAnswer.gameObject.SetActive(true);
    }

    private void DeleteAnswer()
    {
        if(_currentAnswer.Length <= 0)return;
        _currentAnswer =  _currentAnswer.Remove(_currentAnswer.Length - 1);
        _txtAnswer.SetText(_currentAnswer);
        if(_currentAnswer.Length <= 0) _btnCheckAnswer.gameObject.SetActive(false);
    }

    private void SetUpCharacterForAnswer()
    {
        var shuffeledChoice = StaticShuffler.ShuffleList<string>(_currentQuiz.ChoiceList);
        if(charObjList.Count < shuffeledChoice.Count)
        {
            int tempcharObjList = charObjList.Count;
            for (int i=0; i< Mathf.Abs(shuffeledChoice.Count - tempcharObjList); i++)
            {
                CharObj newObj = Instantiate(_charObjPrefab, _charObjPrefabParent);
                charObjList.Add(newObj);
            }
        }

        for(int i=0; i< charObjList.Count; i++)
        {
            if(i < shuffeledChoice.Count)
            {
                charObjList[i].SetUpChar(shuffeledChoice[i]);
            }
            else
            {
                charObjList[i].gameObject.SetActive(false);
            }
            
        }
    }

    private void SetUpQuiz()
    {
        _health = _maxHealth;
        StaticAction.ResetHeart?.Invoke();

        ShowQuestion();
        SetUpAnswer();
    }

    private void CheckAnswer()
    {
        if (_txtAnswer.text.ToLower() == _currentQuiz.Answer.ToLower())
        {
            StaticAction.OnTrueAnswer?.Invoke();
        }
        else
        {
            
            StaticAction.OnWrongAnswer?.Invoke();
        }
    }

    private void NextQuestion()
    {

        if (_currentIndex + 1 <= _questionData.Count - 1)
        {
            _currentIndex++;
            _currentQuiz = _questionData[_currentIndex];
            SetUpQuiz();
        }
        else
        {
            StaticAction.Win(_currentLevelName);
            StaticPlayerPref.SavePlayerData(PlayerData.CurrentLevelIndex);
        }
    }

    private void AnswerWrong()
    {
        _health -= 1;
        StaticAction.ReduceHeart?.Invoke(_health);
        if(_health<= ConstVar.ZERO) StaticAction.Lose(_currentLevelName);
    }


}
