using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector2 travelPoint;
    [SerializeField] private Vector2 selectionBounds;

    private void Awake()
    {
        health = gameObject.GetComponent<Health>();
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        selectionBounds = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        PickTravelLocation();

    }
    //Pick Location to Travel to
    public void PickTravelLocation()
    {
        float positionY = Random.Range(-selectionBounds.y, selectionBounds.y);
        float positionX = Random.Range(-selectionBounds.x, selectionBounds.x);
        travelPoint = new Vector2(positionX, positionY);
    }

    private void Update()
    {
        Move();
        transform.position = Vector2.Lerp(transform.position, travelPoint, 5 * Time.deltaTime);
    }

    [SerializeField] private float MoveTime;
    private float MoveCounter;
    private void Move()
    {
        MoveCounter += Time.deltaTime;
        if (MoveCounter >= MoveTime)
        {
            PickTravelLocation();
            MoveCounter = 0;
        }
    }
    //Damage to the Enemy
    private Health health;
    public void TakeDamage(float damageAmount)
    {
        health.hitpoints = health.hitpoints - damageAmount;
        health.HealthCheck();
    }
}
