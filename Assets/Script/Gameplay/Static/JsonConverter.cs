using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public static class JsonConverter
{
    


    public static T ReadFromJSON<T>(string value)
    {

        
        if (string.IsNullOrEmpty(value) || value == "{}")
        {
            return default(T);
        }

        T data = JsonConvert.DeserializeObject<T>(value);

        //T data = JsonUtility.FromJson<T>(content);
        return data;
    }
}

[Serializable]
public class LevelData
{
    public string LevelID;
    public string LevelName;
    public List<QuestionData> questionData;
}

[Serializable]
public class QuestionData
{
    public string ID;
    public string Question;
    public string Answer;
    public string Alasan;
    public string Clue;
    public string Help;
    public string Choice;
    public List<string> ChoiceList;

}