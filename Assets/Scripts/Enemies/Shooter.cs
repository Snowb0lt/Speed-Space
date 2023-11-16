using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    [Header("Shooter Specific")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int NumberofShots;
    public bool hasFired = false;

    [Header("Shooting")]
    [SerializeField] private AudioSource shotSound;
    private void Shoot()
    {
        for (int i = 0; i < NumberofShots; i++)
        {
            Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
            shotSound.Play();
        }
    }
    public override void Update()
    {
        base.Update();
        Attack(Shoot);   
    }

}
