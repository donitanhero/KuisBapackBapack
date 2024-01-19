using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharObj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtChar;
    [SerializeField] private Button _btnAnswer;

    private string _currentChar;

    private void Awake()
    {
        _btnAnswer.onClick.RemoveAllListeners();
        _btnAnswer.onClick.AddListener(Answering);
    }
    private void Answering()
    {
        StaticAction.GiveAnswer?.Invoke(_currentChar);
    }

    public void SetUpChar(string answerChar)
    {
        _currentChar = answerChar;
        gameObject.SetActive(true);
        _txtChar.SetText(_currentChar);
    }

    public void HideObj()
    {
        gameObject.SetActive(false);
    }

}
