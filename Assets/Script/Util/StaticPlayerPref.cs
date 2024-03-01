using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticPlayerPref 
{
    private const string KEY = "Player_Last_Level";
    
    public static void SavePlayerData(int lastLevel){
        PlayerPrefs.SetInt(KEY, lastLevel);
    }

    public static int GetPlayerData(){
        if(PlayerPrefs.HasKey(KEY)){
            return PlayerPrefs.GetInt(KEY);
        }
        else{
            return -1;
        }
    }

}
 