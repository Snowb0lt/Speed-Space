using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using static UnityEngine.EventSystems.EventTrigger;
using System;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text EntryName, EntryScore;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject EntryPrefab;
    [SerializeField] private Transform HighScoreHolder;

    public Dictionary<string, int> ScoreEntries = new Dictionary<string,int>();

    public static HighScoreManager _instance;
    private void Awake()
    {
        if (_instance == null || _instance != this)
        {
            _instance = this;
        }
    }

    public void AddEntry()
    {
        if (nameInputField.text == "")
        {
            string DefaultName = "Lord Galactic X";
            if (ScoreEntries.ContainsKey(DefaultName))
            {
                ScoreEntries.Remove(DefaultName);
            }
            ScoreEntries.Add(DefaultName, GameManager._instance.PlayerScore);
        }
        else
        {
            if (ScoreEntries.ContainsKey(nameInputField.text))
            {
                ScoreEntries.Remove(nameInputField.text);
            }
            ScoreEntries.Add(nameInputField.text, GameManager._instance.PlayerScore);
        }
        DisplayHighScores();
    }
    [SerializeField] private GameObject HighScoreObject;
    [SerializeField] private List<int> scores;
    public void DisplayHighScores()
    {
        var sorted = ScoreEntries.OrderByDescending(key => key.Value);

        foreach(Transform child in HighScoreHolder)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (var entry in sorted.Take(5))
        {
            Instantiate(EntryPrefab, HighScoreHolder);
            EntryName = EntryPrefab.transform.GetChild(0).GetComponent<TMP_Text>();
            EntryScore = EntryPrefab.transform.GetChild(1).GetComponent<TMP_Text>();
            EntryName.text = entry.Key;
            EntryScore.text = entry.Value.ToString();
        }
        HighScoreObject.SetActive(true);
        SavingManager._instance.SaveScores(ScoreEntries);
    }
}
