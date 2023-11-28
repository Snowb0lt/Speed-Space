using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    private UIManager playerUI;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null || _instance != this)
        {
            _instance = this;
        }
    }
    private void Start()
    {
        playerUI = GameObject.FindAnyObjectByType<UIManager>().GetComponent<UIManager>();
        player = GameObject.FindWithTag("Player");
        BossReset();
        playerSpawnPoint = GameObject.FindWithTag("PlayerSpawn");
    }
    private int enemyToSpawn;
    void LateUpdate()
    {
        EnemySpawning();

        //VERY ROUGH ESCAPE KEY EXIT. REPLACE LATER
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    //Spawning Mechanics
    [SerializeField] private List<GameObject> EnemyList = new List<GameObject>();
    private object enemyCheck;
    private object BossCheck;
    [SerializeField] private int baseSpawningNum;
    private int enemiesToSpawn;
    public List<GameObject> spawnAreas = new List<GameObject>();
    private float DelayTimer;
    private float SpawnMoment = 2;
    private void EnemySpawning()
    {
        //Check if enemies are on screen
        enemyCheck = FindAnyObjectByType(typeof(Enemy));
        BossCheck = FindAnyObjectByType(typeof(Boss));
        if (enemyCheck == null && BossCheck == null)
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
    //[SerializeField] private Boss bossScript;
    public void NextRound()
    {
        RoundsBeforeBoss--;
        if (RoundsBeforeBoss == 0)
        {
            SpawnBoss();
        }
    }
    [Header("Boss Spawning/Intro")]
    private AudioSource BossSpawnSound;
    private List<GameObject> BossList = new List<GameObject>();
    private int randomNumber;
    private GameObject BossSpawnLocation;
    public void SpawnBoss()
    {
        randomNumber = Random.Range(0, BossList.Count);
        Instantiate(BossList[randomNumber], BossSpawnLocation.transform);
    }
    public void BossReset()
    {
        RoundsBeforeBoss = 3 + Random.Range(0, 5);
    }

    public int NumberOfLives = 3;
    public void LoseALife()
    {
        NumberOfLives--;
        UIManager._instance.UpdateLives();
        Invoke("Respawn", 5);
    }
    [Header("Respawn Mechanics")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSpawnPoint;
    private Player playerScript;
    public void Respawn()
    {
        player.transform.position = playerSpawnPoint.transform.position;
        playerScript = player.GetComponent<Player>();
        playerScript.health.hitpoints = 1;
        playerScript.currentShieldAmount = playerScript.maxShieldAmount;
        player.SetActive(true);
    }

    //Bomb Storage Mechanics
    [Header("Bomb Mechanics")]
    public int NumberOfBombs;
    public int MaxBombCount;
    [SerializeField] private Bomb bomb;
    public void UseBomb()
    {
        if (NumberOfBombs > 0)
        {
            bomb.ActivateBomb();
            NumberOfBombs--;
            UIManager._instance.UpdateBombCount(NumberOfBombs);
            AddScore(200);
        }
    }

}
