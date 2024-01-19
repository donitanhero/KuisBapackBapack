using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManagers : MonoBehaviour
{
    private const string TXT_CLUE = "Clue: ";

    [SerializeField] private Level_SO _levelData;
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

    private Quiz_SO _currentQuiz;
    private int _currentIndex;
    private List<CharObj> charObjList = new List<CharObj>();
    private string _currentAnswer;

    private void Awake()
    {
        _currentIndex = -1;
        _btnCheckAnswer.onClick.AddListener(CheckAnswer);
        _btnDeleteAnswer.onClick.AddListener(DeleteAnswer);
        _btnHelp.onClick.AddListener(Help);

        StaticAction.GiveAnswer = GiveAnswer;
        StaticAction.NextQuestion = NextQuestion;

        NextQuestion();

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
        _currentAnswer =  _currentAnswer.Remove(_currentAnswer.Length - 1);
        _txtAnswer.SetText(_currentAnswer);
    }

    private void SetUpCharacterForAnswer()
    {
        if(charObjList.Count < _currentQuiz.CharChoiceList.Length)
        {
            int tempcharObjList = charObjList.Count;
            for (int i=0; i< Mathf.Abs(_currentQuiz.CharChoiceList.Length - tempcharObjList); i++)
            {
                CharObj newObj = Instantiate(_charObjPrefab, _charObjPrefabParent);
                charObjList.Add(newObj);
            }
        }

        for(int i=0; i< charObjList.Count; i++)
        {
            if(i < _currentQuiz.CharChoiceList.Length)
            {
                charObjList[i].SetUpChar(_currentQuiz.CharChoiceList[i]);
            }
            else
            {
                charObjList[i].gameObject.SetActive(false);
            }
            
        }
    }

    private void SetUpQuiz()
    {

        ShowQuestion();
        SetUpAnswer();
    }

    private void CheckAnswer()
    {
        if (_txtAnswer.text == _currentQuiz.Answer)
        {
            _pnlTrueAnswer.SetActive(true);
            
        }
        else
        {
            Debug.Log("Salah");
        }
    }

    private void NextQuestion()
    {

        if (_currentIndex + 1 <= _levelData.QuizList.Count - 1)
        {
            _currentIndex++;
            _currentQuiz = _levelData.QuizList[_currentIndex];
            SetUpQuiz();
        }
        else
        {
            Debug.Log("Game Selesai");
        }
    }

}
