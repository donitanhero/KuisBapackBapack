using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticShuffler 
{

    public static List<T> ShuffleList<T>(List<T> listToShuffle)
    {
        for (int i = 0; i < listToShuffle.Count; i++)
        {
            T temp = listToShuffle[i];
            int randomIndex = Random.Range(i, listToShuffle.Count);
            listToShuffle[i] = listToShuffle[randomIndex];
            listToShuffle[randomIndex] = temp;
        }
        return listToShuffle;
    }
}
