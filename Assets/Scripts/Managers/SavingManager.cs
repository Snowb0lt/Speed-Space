using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class SavingManager : MonoBehaviour
{
    private HighScores scoresToSave;
    private IDataMangement dataMangement;

    public static SavingManager _instance;
    private void Awake()
    {
        if (_instance == null || _instance != this)
        {
            _instance = this;
        }
    }
    private void SaveScores()
    {
        
    }

    private void LoadScores()
    {

    }
}

[Serializable]
public class HighScores
{
    Dictionary<string, int> highScoresToSave;
}
