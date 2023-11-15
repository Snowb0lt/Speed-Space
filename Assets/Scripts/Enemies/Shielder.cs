using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shielder : Enemy
{
    private Vector2 selectionBounds;
    [SerializeField] private GameObject Spawnarea;
    public override void Awake()
    {
        base.Awake();
        Spawnarea = GameObject.FindWithTag("ShielderSpawn");
        EnemyFacing();
    }

    public override void Update()
    {
        Move();
        transform.position = Vector2.MoveTowards(transform.position, travelPoint, 3 * Time.deltaTime);
        RechargeShield();
    }

    private void RechargeShield()
    {
        if (ShieldObject.activeSelf == false)
        {
            ShieldTime += Time.deltaTime;
            if (ShieldTime >= RechargeMoment)
            {
                ActivateShield();
                ShieldTime = 0;
            }
        }
    }

    public override void PickTravelLocation()
    {
        var Spawnbounds = Spawnarea.GetComponent<SpriteRenderer>().bounds;
        travelPoint = new Vector2 (Random.Range(Spawnbounds.min.x, Spawnbounds.max.x), Spawnarea.transform.position.y);
    }
    public override void EnemyFacing()
    {
        transform.rotation = Quaternion.identity;
    }

    [Header("Shields")]
    [SerializeField]private GameObject ShieldObject;
    [SerializeField]private int ShieldHP;
    [SerializeField]private float ShieldTime;
    [SerializeField]private float RechargeMoment = 5;
    private void ActivateShield()
    {
        ShieldObject.SetActive (true);
    }
    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        if (ShieldObject.activeSelf == true)
        {
            ShieldHP--;
            if (ShieldHP <= 0)
            {
                ShieldObject.SetActive(false);
            }
        }
    }
}
