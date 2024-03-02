using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using static UnityEngine.EventSystems.EventTrigger;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text EntryName, EntryScore;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject EntryPrefab;
    [SerializeField] private Transform HighScoreHolder;

    public SortedDictionary<string, int> ScoreEntries = new SortedDictionary<string,int>();

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
            ScoreEntries.Add("Lord Galactic X", GameManager._instance.PlayerScore);
        }
        else
        {
            ScoreEntries.Add(nameInputField.text, GameManager._instance.PlayerScore);
        }
        DisplayHighScores();
    }

    [SerializeField] private GameObject HighScoreObject;
    public void DisplayHighScores()
    {
        foreach (var entry in ScoreEntries.OrderBy(key => key.Value).Take(10))
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
