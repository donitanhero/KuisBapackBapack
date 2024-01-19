using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Quiz Data", menuName = "ScriptableObjects/Quiz", order = 1)]
public class Quiz_SO : ScriptableObject
{
    public string Question;
    public string Answer;
    public string Clue;
    public string Help;
    public string[] CharChoiceList;
}
