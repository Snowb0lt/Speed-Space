using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private UIManager playerUI;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        playerUI = GameObject.FindAnyObjectByType<UIManager>().GetComponent<UIManager>();
        BossReset();
    }
    private int enemyToSpawn;
    // Update is called once per frame
    void LateUpdate()
    {
        EnemySpawning();
    }


    //Spawning Mechanics
    [SerializeField] private List<GameObject> EnemyList = new List<GameObject>();
    private object enemyCheck;
    [SerializeField] private int baseSpawningNum;
    private int enemiesToSpawn;
    [SerializeField]private List<GameObject> spawnAreas = new List<GameObject>();
    private float DelayTimer;
    private float SpawnMoment = 2;
    private void EnemySpawning()
    {
        //Check if enemies are on screen
        enemyCheck = FindAnyObjectByType(typeof(Enemy));
        if (enemyCheck == null)
        {
            //break between spawns
            //BackgroundScroll.instance.scrollSpeed = 15;
            enemiesToSpawn = Random.Range(baseSpawningNum, baseSpawningNum + 3);
            DelayTimer += Time.deltaTime;
            if (DelayTimer >= SpawnMoment)
            {
                NextRound();    
                //BackgroundScroll.instance.scrollSpeed = 10;

                //"Ambush" Spawn
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    enemyToSpawn = Random.Range(0, EnemyList.Count);
                    Instantiate(EnemyList[enemyToSpawn], spawnAreas[Random.Range(0, spawnAreas.Count)].transform.position, Quaternion.identity);
                }
                DelayTimer = 0;
            } 
        }
    }
    private int PlayerScore;
    public void AddScore(int scoreamount)
    {
        PlayerScore = PlayerScore + scoreamount;
        playerUI.scoretext.text = "Score: " + PlayerScore.ToString();
    }
    //Rounds and Bosses
    [SerializeField] private int RoundsBeforeBoss;
    public void NextRound()
    {
        RoundsBeforeBoss--;
        if (RoundsBeforeBoss == 0)
        {
            Debug.Log("BossHasSpawned");
        }
    }
    public void BossReset()
    {
        RoundsBeforeBoss = 3 + Random.Range(0, 5);
    }
}
