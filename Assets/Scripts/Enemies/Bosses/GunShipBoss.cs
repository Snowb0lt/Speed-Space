using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunShipBoss : Boss
{
    //private void BossMovement()
    //{
    //    var Spawnbounds = Spawnarea.GetComponent<SpriteRenderer>().bounds;
    //    travelPoint = new Vector2(UnityEngine.Random.Range(Spawnbounds.min.x, Spawnbounds.max.x), Spawnarea.transform.position.y);
    //}

    private Health turretHealth;
    private void Awake()
    {
        bossAttacks.Add(AttackShoot);
    }
    public override void Start()
    {
        base.Start();
        
        //bossAttacks.Add(AttackLaser);
        //foreach (var laser in LaserPorts)
        //{
        //    laser.SetActive(false);
        //}
        foreach(var turret in Turrets)
        {
            turretHealth = turret.GetComponent<Health>();
        }
    }

    public override void Update()
    {
        base.Update();
        TurretMovement();
        //if (Input.GetKeyDown(KeyCode.P)) 
        //{
        //    AttackLaser();
        //}
        Attack(bossAttacks[UnityEngine.Random.Range(0, bossAttacks.Count)]);
    }

    [Header("Weapons")]
    public List<GameObject> Turrets = new List<GameObject>();

    [SerializeField] private GameObject bulletPrefab;
    //Concerning the gunship's cannons
    private void TurretMovement()
    {  
        foreach (GameObject turret in Turrets)
        {
            if (turret != null)
            {
                Vector3 targetPos = target.transform.position - turret.transform.position;
                float Facing = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg + 90;
                turret.transform.rotation = Quaternion.Euler(0, 0, Facing);
            }
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

    //[SerializeField] private List<GameObject> LaserPorts = new List<GameObject>();
    //private Color laserColor;
    //private Animator LaserAnimation;
    //[SerializeField] private GameObject gunshipLaser;
    //[SerializeField] private AnimatorController laserController;
    ////fire the twin lasers
    //private void AttackLaser()
    //{
    //    foreach (GameObject laser in LaserPorts)
    //    {
    //        Instantiate(gunshipLaser, laser.transform.position, Quaternion.identity);
    //        Invoke("FireLaser", 2);
    //    }
    //}

    //private void FireLaser()
    //{
    //    Debug.Log("Laser would fire here");
    //    foreach (GameObject laser in LaserPorts)
    //    {
    //        gunshipLaser.GetComponent<Animator>().Play("GunshipLaserAnime");
    //    }
    //}

    public override void TakeDamage(float damageAmount)
    {
        if (Turrets.Count != 0)
        {
            Debug.Log("Destroy the Turrets First");
        }
        else
        {
            base.TakeDamage(damageAmount);
        }
    }
}
