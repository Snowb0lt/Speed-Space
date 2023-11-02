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

    public override void Update()
    {
        base.Update();
    }

    private void Shoot()
    {
        if (Rb.velocity.magnitude <= 0 && !hasFired)
        {
            for (int i = 0; i < NumberofShots; i++)
                {
                    Debug.Log("Bullet Fired");
                    Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
                }
            hasFired = true;
        }
    }

    public override void Move()
    {
        if (hasFired)
        {

            base.Move();
            Shoot();
        }
    }
}
