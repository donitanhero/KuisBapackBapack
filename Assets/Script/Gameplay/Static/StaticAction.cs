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

    public static Action OnTrueAnswer;

    #endregion

    #region Question Fetcher

    public static Action<Action<List<LevelData>>> GetLevelData;

    public static Action<bool> OnInternetConnectionState;

    public static bool IsQuestionFetched;

    #endregion

    #region LoadingScreen

    public static Action<int> OnSceneChange;

    #endregion

    #region UI Manager

    public static Action<int> ReduceHeart;
    public static Action ResetHeart;
    public static Action<string> Win;
    public static Action<string> Lose;

    
    

    #endregion

    #region Sound

    public static Action<int> OnMusicPlay;
    public static Action<int> OnSFXSoundPlay;
    #endregion
}
