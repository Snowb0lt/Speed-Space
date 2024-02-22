using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text EntryName, EntryScore;
    [SerializeField] private GameObject EntryPrefab;
    [SerializeField] private Transform HighScoreHolder;

    [SerializeField] private Dictionary<string, int> ScoreEntries;

    public void DisplayHighScores()
    {
        foreach (var entry in ScoreEntries)
        {
            Instantiate(EntryPrefab, HighScoreHolder);
            EntryName = EntryPrefab.transform.GetChild(0).GetComponent<TMP_Text>();
            EntryScore = EntryPrefab.transform.GetChild(1).GetComponent<TMP_Text>();
            EntryName.text = entry.Key;
            EntryScore.text = entry.Value.ToString();
        }
    }
}
