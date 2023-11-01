using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject bullet;
    [SerializeField] private int bulletSpeed = 20;
    private float bulletLifespan = 2;
    [SerializeField] private float lifespanCounter;
    [SerializeField] private float damageAmount;
    // Start is called before the first frame update
    void Awake()
    {
        bullet = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        BulletMechanics();
    }

    private void BulletMechanics()
    {
        bullet.transform.position = bullet.transform.position + (Vector3.up * bulletSpeed) * Time.deltaTime;
        lifespanCounter += Time.deltaTime;
        if (lifespanCounter >= bulletLifespan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable hit))
        {
            hit.TakeDamage(damageAmount);
        }
    }
}
