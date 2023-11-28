using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunShipBoss : Boss
{
    private void BossMovement()
    {
        var Spawnbounds = Spawnarea.GetComponent<SpriteRenderer>().bounds;
        travelPoint = new Vector2(Random.Range(Spawnbounds.min.x, Spawnbounds.max.x), Spawnarea.transform.position.y);
    }

    public override void Update()
    {
        base.Update();
        Vector3 Look = transform.InverseTransformPoint(target.transform.position);
        float Angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg;
        foreach (GameObject turret in Turrets)
        {
            turret.transform.rotation = Quaternion.Euler(0,0,Angle + 90);
        }
    }

    [Header("Weapons")]
    [SerializeField]private List<GameObject> Turrets = new List<GameObject>();
    [SerializeField]private List<GameObject> LaserPorts = new List<GameObject>();
    [SerializeField] private GameObject bulletPrefab;
    //Fire the gunship's cannons
    private void AttackShoot()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
        }
    }

    //fire the twin lasers
    private void AttackLaser()
    {

    }
}
