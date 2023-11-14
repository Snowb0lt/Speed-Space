using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private void Awake()
    {
        
    }
    //Damage To Boss
    [Header("Health")]
    public Health health;
    public void TakeDamage(float damageAmount)
    {
        health.hitpoints = health.hitpoints - damageAmount;
        health.HealthCheck();
    }

    [Header("Spawning/Intro")]
    private AudioSource BossSpawnSound;
    private List<GameObject> BossList = new List<GameObject>();
    private int randomNumber;
    private GameObject BossSpawnLocation;
    public void Spawn()
    {
        randomNumber = Random.Range(0, BossList.Count);
        Instantiate(BossList[randomNumber], BossSpawnLocation.transform);
    }
}
