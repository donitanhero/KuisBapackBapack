using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class StaticAction
{
    #region Quiz Manager

    public static Action<string> GiveAnswer;
    public static Action NextQuestion;

    public static Action OnWrongAnswer;

    #endregion

    #region Question Fetcher

    public static Action<Action<List<LevelData>>> GetLevelData;

    public static Action<bool> OnInternetConnectionState;

    public static bool IsQuestionFetched;

    #endregion

    #region LoadingScreen

    public static Action<int> OnSceneChange;

    #endregion

}
