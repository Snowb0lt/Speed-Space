using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

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
    private Vector2 screenBoundaries;
    private float halfPlayerWidth;
    public virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        selectionBounds = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        PickTravelLocation();
        destruction = GameObject.FindWithTag("Explosion").GetComponent<AudioSource>();

        //Binds them within the screen
        halfPlayerWidth = transform.localScale.x / -2;
        screenBoundaries = new Vector2(Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth, Camera.main.orthographicSize);

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
            EnemyBoudaries();
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
    //Keeps the Enemy inside the bounds of the screen
    private void EnemyBoudaries()
    {
        if (!EnemyLeaving)
        {
            if (transform.position.x < -screenBoundaries.x)
            {
                transform.position = new Vector2(-screenBoundaries.x, transform.position.y);
            }
            else if (transform.position.x > screenBoundaries.x)
            {
                transform.position = new Vector2(screenBoundaries.x, transform.position.y);
            }
            if (transform.position.y < -screenBoundaries.y)
            {
                transform.position = new Vector2(transform.position.x, -screenBoundaries.y);
            }
            else if (transform.position.y > screenBoundaries.y)
            {
                transform.position = new Vector2(transform.position.x, screenBoundaries.y);
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
    private float LingerTime = 15;
    private float LingerCount = 0;
    private bool EnemyLeaving = false;
    public virtual void Move()
    {
        //Timers for movement/Leaving
        MoveCounter += Time.deltaTime;
        LingerCount += Time.deltaTime;
        if (MoveCounter >= MoveTime)
        {
            if (LingerCount < LingerTime)
            {
                PickTravelLocation();
                MoveCounter = 0;
            }
            if (LingerCount >= LingerTime && !EnemyLeaving)
            {
                Debug.Log("Enemy Is Leaving");
                EnemyLeaving=true;
                travelPoint = GameManager._instance.spawnAreas[UnityEngine.Random.Range(0, GameManager._instance.spawnAreas.Count)].transform.position;
                transform.position = Vector2.Lerp(transform.position, travelPoint, 5 * Time.deltaTime);
            }
        }
        if (EnemyLeaving)
        if (Vector3.Distance(transform.position, travelPoint) <= 0.5f)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    //Damage to the Enemy
    [Header("Health")]
    public Health health;
    public virtual void TakeDamage(float damageAmount)
    {
        health.hitpoints = health.hitpoints - damageAmount;
        health.HealthCheck();
        if (health.hitpoints <= 0) 
        {
            destruction.Play();
        }
    }

    public float attackRate;
    private float attackCooldown = 0;
    public virtual void Attack(Action Attack)
    {
        if (target.gameObject.activeSelf == true && !EnemyLeaving)
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

    public void Death()
    {
        GameManager._instance.AddScore(enemyScore);
        Destroy(this.gameObject);
    }
}
