using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public GameObject target;
    public Vector3 travelPoint;
    private Vector2 selectionBounds;
    protected Rigidbody2D Rb;

    public int enemyScore;
    public AudioSource destruction;

    public virtual void Awake()
    {
        health = gameObject.GetComponent<Health>();
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        selectionBounds = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        PickTravelLocation();
        destruction = GameObject.FindWithTag("Explosion").GetComponent<AudioSource>();

    }
    //Pick Location to Travel to
    public virtual void PickTravelLocation()
    {
        float positionY = UnityEngine.Random.Range(-selectionBounds.y, selectionBounds.y);
        float positionX = UnityEngine.Random.Range(-selectionBounds.x, selectionBounds.x);
        travelPoint = new Vector2(positionX, positionY);
    }

    public virtual void Update()
    {
        if (target != null)
        {
            Move();
            transform.position = Vector2.Lerp(transform.position, travelPoint, 5 * Time.deltaTime);
            EnemyFacing();
        }
        else
        {
            try
            {
                target = GameObject.FindGameObjectWithTag("Player");
            }
            catch
            {
                return;
            }
        }
    }

    public virtual void EnemyFacing()
    {
        Vector3 Look = transform.InverseTransformPoint(target.transform.position);
        float Angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg;

        transform.Rotate(0, 0, Angle);
    }

    [Header("Movement")]
    public float MoveTime = 3;
    protected float MoveCounter;
    public virtual void Move()
    {
        MoveCounter += Time.deltaTime;
        if (MoveCounter >= MoveTime)
        {
            PickTravelLocation();
            MoveCounter = 0;
        }
    }
    //Damage to the Enemy
    [Header("Health")]
    public Health health;
    public virtual void TakeDamage(float damageAmount)
    {
        health.hitpoints = health.hitpoints - damageAmount;
        health.HealthCheck();
    }
    private void OnDestroy()
    {
        destruction.Play();
    }

    public float attackRate;
    private float attackCooldown = 0;
    public virtual void Attack(Action Attack)
    {
        if (target.gameObject.activeSelf == true)
        {
            attackCooldown += Time.deltaTime;
            if (attackCooldown >= attackRate)
            {
                Attack?.Invoke();
                Move();
                attackCooldown = 0;
            }
        }
        else
        {
            attackCooldown = 0;
        }
    }
}
