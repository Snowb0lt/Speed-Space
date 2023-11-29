using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    public GameObject target;
    public GameObject Spawnarea;
    public List<Action> bossAttacks;
    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
        health = GetComponent<Health>();
    }
    [Header("Movement")]
    [SerializeField] private GameObject BossStartMove;
    public virtual void Start()
    {
        bossAttacks = new List<Action>();
        travelPoint = BossStartMove.transform.position;
    }
    public int BossMoveSpeed;
    public virtual void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, travelPoint, BossMoveSpeed * Time.deltaTime);
    }
    public Vector2 travelPoint;

    //Damage To Boss
    [Header("Health")]
    public Health health;
    public void TakeDamage(float damageAmount)
    {
        health.hitpoints = health.hitpoints - damageAmount;
        health.HealthCheck();
    }

    public float attackRate;
    public float attackCooldown = 0;
    public virtual void Attack(Action Attack)
    {
        if (target.gameObject.activeSelf == true)
        {
            attackCooldown += Time.deltaTime;
            if (attackCooldown >= attackRate)
            {
                Attack?.Invoke();
                attackCooldown = 0;
            }
        }
        else
        {
            attackCooldown = 0;
        }
    }
}
