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
        mainHealth = GetComponent<Health>();
    }
    [Header("Movement")]
    [SerializeField] private GameObject BossStartMove;
    private Color BossColor;
    public virtual void Start()
    {
        bossAttacks = new List<Action>();
        travelPoint = BossStartMove.transform.position;
        BossColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }
    public int BossMoveSpeed;
    public virtual void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, travelPoint, BossMoveSpeed * Time.deltaTime);
    }
    public Vector2 travelPoint;

    //Damage To Boss
    [Header("Health")]
    public Health mainHealth;
    public virtual void TakeDamage(float damageAmount)
    {
        mainHealth.hitpoints = mainHealth.hitpoints - damageAmount;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        this.gameObject.GetComponent<SpriteRenderer>().color = BossColor;
        mainHealth.HealthCheck();
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

    public void Death()
    {
        GameManager._instance.BossReset();
        Destroy(this.gameObject);
    }
}
