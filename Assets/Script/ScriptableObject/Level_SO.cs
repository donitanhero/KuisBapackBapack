using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz Data", menuName = "ScriptableObjects/Level", order = 1)]
public class Level_SO : ScriptableObject
{
    public List<Quiz_SO> QuizList;
}
