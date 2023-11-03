using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> EnemyList = new List<GameObject>();
    private object enemyCheck;
    [SerializeField] private int enemiesToSpawn;
    [SerializeField]private List<GameObject> spawnAreas = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }
    private int enemyToSpawn;
    // Update is called once per frame
    void LateUpdate()
    {
        enemyCheck = FindAnyObjectByType(typeof(Enemy));
        if (enemyCheck == null)
        {
            for(int i = 0; i < enemiesToSpawn; i++)
            {
                enemyToSpawn = Random.Range(0, EnemyList.Count);
                Instantiate(EnemyList[enemyToSpawn], spawnAreas[Random.Range(0,spawnAreas.Count)].transform.position, Quaternion.identity);
            }
        }
    }
}
