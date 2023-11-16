using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Animations;

public class Bomber : Enemy
{
    public override void Update()
    {
        Move();
        transform.position = Vector2.MoveTowards(transform.position, travelPoint, 3 * Time.deltaTime);
        Attack(LayMine);
    }

    public override void PickTravelLocation()
    {
        base.PickTravelLocation();
        EnemyFacing();
    }
    public override void EnemyFacing()
    {
        Vector3 Look = transform.InverseTransformPoint(travelPoint);
        float Angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg + 90;

        transform.Rotate(0, 0, Angle);
    }
    [SerializeField] private GameObject MinePrefab;
    public void LayMine()
    {
        Instantiate(MinePrefab, transform.position, Quaternion.identity);
    }
}
