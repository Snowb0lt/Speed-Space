using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>().GetComponent<Player>();
        color = ShieldIcon.color;
        SpawnPosition = LivesHolder.transform;
    }

    [SerializeField] private Image ShieldIcon;
    [SerializeField] private GameObject ShipIcon;
    private Color color;

    // Update is called once per frame
    void Update()
    {
        UpdateShields();
        if (player.health.hitpoints <= 0)
        {
            ShipIcon.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            NumberOfLives--;
        }
        UpdateLives();
    }

    private void UpdateShields()
    {
        ShieldIcon.color = color;
        color.a = player.currentShieldAmount / player.maxShieldAmount;
    }

    public TMP_Text scoretext;

    [Header("Lives System")]
    [SerializeField] private GameObject LifeUIObject;
    [SerializeField] private int NumberOfLives;
    [SerializeField] private GameObject LivesHolder;
    private Transform SpawnPosition;
    [SerializeField] private List<GameObject> Lives = new List<GameObject>();
    private void UpdateLives()
    { 
        for (int i = 0; i < Lives.Count; i++)
        {   
            if (i < NumberOfLives)
            {
                Lives[i].SetActive(true);
            }
            else
            {
                Lives[i].SetActive(false);
            }
        }
    }
}
