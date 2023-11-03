using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

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
    [SerializeField] private int enemiesToSpawn;
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
            
            DelayTimer += Time.deltaTime;
            if (DelayTimer >= SpawnMoment)
            {
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    enemyToSpawn = Random.Range(0, EnemyList.Count);
                    Instantiate(EnemyList[enemyToSpawn], spawnAreas[Random.Range(0, spawnAreas.Count)].transform.position, Quaternion.identity);
                }
                DelayTimer = 0;
            } 
        }
    }
}
