using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shield : Enemy
{
    private Vector2 selectionBounds;
    [SerializeField] private GameObject Spawnarea;
    public override void Awake()
    {
        base.Awake();
        Spawnarea = GameObject.FindWithTag("ShielderSpawn");
    }
    public override void PickTravelLocation()
    {
        var Spawnbounds = Spawnarea.GetComponent<SpriteRenderer>().bounds;
        travelPoint = new Vector2 (Random.Range(Spawnbounds.min.x, Spawnbounds.max.x), Spawnarea.transform.position.y);
    }
    public override void EnemyFacing()
    {
        
    }
}
