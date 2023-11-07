using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    [Header("Shooter Specific")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int NumberofShots;
    public bool hasFired = false;
    // Start is called before the first frame update

    [Header("Shooting")]
    [SerializeField] private float fireRate;
    [SerializeField] private float fireCooldown = 0;
    [SerializeField] private AudioSource shotSound;
    private void Shoot()
    {
        fireCooldown += Time.deltaTime;
        if (fireCooldown >= fireRate && Rb.velocity.magnitude <= 0)
        {
            for (int i = 0; i < NumberofShots; i++)
            {
                Debug.Log("Bullet Fired");
                Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                shotSound.Play();
            }
            Move();
            fireCooldown = 0;
        }
    }
    public override void Update()
    {
        base.Update();
        Shoot();   
    }

}
