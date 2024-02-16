using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelObj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtLevel;
    [SerializeField] private Button _btnPlayLevel;

    private int _levelIndex;

    private void Awake()
    {
        _btnPlayLevel.onClick.AddListener(LevelPlay);
    }

    public void SetUp(int index)
    {
        _levelIndex = index;
        _txtLevel.SetText((index + ConstVar.ONE).ToString());
    }

    private void LevelPlay()
    {
        PlayerData.CurrentLevelIndex = _levelIndex;
        StaticAction.OnSceneChange?.Invoke(ConstVar.GAMEPLAY_SCENE);
    }

}
