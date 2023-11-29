using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunShipBoss : Boss
{
    private void BossMovement()
    {
        var Spawnbounds = Spawnarea.GetComponent<SpriteRenderer>().bounds;
        travelPoint = new Vector2(UnityEngine.Random.Range(Spawnbounds.min.x, Spawnbounds.max.x), Spawnarea.transform.position.y);
    }

    public override void Start()
    {
        base.Start();
        bossAttacks.Add(AttackShoot);
        bossAttacks.Add(AttackLaser);
    }

    public override void Update()
    {
        base.Update();
        TurretMovement();
        Attack(bossAttacks[UnityEngine.Random.Range(0, bossAttacks.Count)]);
    }

    [Header("Weapons")]
    [SerializeField]private List<GameObject> Turrets = new List<GameObject>();

    [SerializeField] private GameObject bulletPrefab;
    //Concerning the gunship's cannons
    private void TurretMovement()
    {
        foreach (GameObject turret in Turrets)
        {
            Vector3 targetPos = target.transform.position - turret.transform.position;
            float Facing = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg + 90;
            turret.transform.rotation = Quaternion.Euler(0, 0, Facing);
        }
    }
    private void AttackShoot()
    {
        foreach (GameObject turret in Turrets)
        {
            StartCoroutine(FireRate(turret));
        }
    }

    IEnumerator FireRate(GameObject turret)
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(bulletPrefab, new Vector3(turret.transform.position.x, turret.transform.position.y, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    [SerializeField] private List<GameObject> LaserPorts = new List<GameObject>();
    //fire the twin lasers
    private void AttackLaser()
    {
        Debug.Log("Laser would fire here");
    }

}
