using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Rendering;

public class SavingManager : MonoBehaviour
{
    public HighScores scoreToSave;
    private IDataMangement dataMangement;

    public static SavingManager _instance;
    private void Awake()
    {
        if (_instance == null || _instance != this)
        {
            _instance = this;
        }
    }
    public void SaveScores(Dictionary<string,int> highScores)
    {
        string ScoresToSave = JsonConvert.SerializeObject(highScores, Formatting.Indented);
        File.WriteAllText(Application.persistentDataPath + "/HighScores.json", ScoresToSave);
    }

    public void LoadScores()
    {
        var loadedScores = File.ReadAllText(Application.persistentDataPath + "/HighScores.json");
        HighScoreManager._instance.ScoreEntries = JsonConvert.DeserializeObject<Dictionary<string, int>>(loadedScores);
    }
}

[Serializable]
public class HighScores
{
    public Dictionary<string, int> highScoresToSave;
}
