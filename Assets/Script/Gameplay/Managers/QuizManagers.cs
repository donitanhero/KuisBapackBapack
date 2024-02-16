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
    [SerializeField] private TextMeshProUGUI _txtQuestion;
    [SerializeField] private TextMeshProUGUI _txtClue;

    [SerializeField] private TextMeshProUGUI _txtAnswer;


    [SerializeField] private Button _btnCheckAnswer;
    [SerializeField] private Button _btnDeleteAnswer;
    [SerializeField] private Button _btnHelp;
    
    [Header("True Answer UI")]
    [SerializeField] private GameObject _pnlTrueAnswer;

    [Header("False Answer UI")]
    [SerializeField] private GameObject _pnlWrongAnswer;

    [Header("Heart")]
    [SerializeField] private TextMeshProUGUI _txtHeart;

    private QuestionData _currentQuiz;
    private int _currentIndex;
    private List<CharObj> charObjList = new List<CharObj>();
    private string _currentAnswer;

    private int _health;

    


    private void Awake()
    {
        _questionData = new List<QuestionData>();
        _currentIndex = -1;
        _btnCheckAnswer.onClick.AddListener(CheckAnswer);
        _btnDeleteAnswer.onClick.AddListener(DeleteAnswer);
        _btnHelp.onClick.AddListener(Help);

        StaticAction.GiveAnswer = GiveAnswer;
        StaticAction.NextQuestion = NextQuestion;
        StaticAction.OnWrongAnswer = AnswerWrong;
        StaticAction.GetLevelData?.Invoke(SetQuestionData);
       

        NextQuestion();
        
        
    }

    private void SetQuestionData(List<LevelData> levelData)
    {
        _questionData = levelData[PlayerData.CurrentLevelIndex].questionData;
    }

    private void Help()
    {
        SetClue(_currentQuiz.Help);
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
    }

    private void DeleteAnswer()
    {
        if(_currentAnswer.Length <= 0) return;
        _currentAnswer =  _currentAnswer.Remove(_currentAnswer.Length - 1);
        _txtAnswer.SetText(_currentAnswer);
    }

    private void SetUpCharacterForAnswer()
    {
        if(charObjList.Count < _currentQuiz.ChoiceList.Count)
        {
            int tempcharObjList = charObjList.Count;
            for (int i=0; i< Mathf.Abs(_currentQuiz.ChoiceList.Count - tempcharObjList); i++)
            {
                CharObj newObj = Instantiate(_charObjPrefab, _charObjPrefabParent);
                charObjList.Add(newObj);
            }
        }

        for(int i=0; i< charObjList.Count; i++)
        {
            if(i < _currentQuiz.ChoiceList.Count)
            {
                charObjList[i].SetUpChar(_currentQuiz.ChoiceList[i]);
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
        UpdateHeartUI();

        ShowQuestion();
        SetUpAnswer();
    }

    private void CheckAnswer()
    {
        if (_txtAnswer.text.ToLower() == _currentQuiz.Answer.ToLower())
        {
            _pnlTrueAnswer.SetActive(true);
            
        }
        else
        {
            _pnlWrongAnswer.SetActive(true);
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
            Debug.Log("Game Selesai");
        }
    }

    private void AnswerWrong()
    {
        _health -= 1;
        UpdateHeartUI();
    }

    private void UpdateHeartUI()
    {
        _txtHeart.SetText("x "+_health.ToString());
    }

}
