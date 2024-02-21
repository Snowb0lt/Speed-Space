using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour, IDamageable
{
    public GameObject target;
    public GameObject Spawnarea;

    public List<System.Action> bossAttacks;
    public virtual void Awake()
    {
        target = GameObject.FindWithTag("Player");
        mainHealth = GetComponent<Health>();
        bossAttacks = new List<System.Action>();
    }
    [Header("Movement")]
    [SerializeField] private GameObject BossStartMove;
    private Color BossColor;
    public virtual void Start()
    { 
        BossColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }
    public int BossMoveSpeed;
    public virtual void Update()
    {
        if (!bossInPosition)
        {
            MoveToPosition();
        }
    }
    public Vector3 travelPoint;

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
    [SerializeField]private int bossScoreValue;
    public void Death()
    {
        GameManager._instance.AddScore(bossScoreValue);
        GameManager._instance.BossReset();
        Destroy(this.gameObject);
    }

    public bool bossInPosition = false;
    public virtual void MoveToPosition()
    {
        if (!bossInPosition && (this.gameObject.transform.position == travelPoint))
        {

        }
        transform.position = Vector2.MoveTowards(transform.position, travelPoint, BossMoveSpeed * Time.deltaTime);
    }
}
