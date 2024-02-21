using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Mine" || collision.gameObject.tag == "BossMain" || collision.gameObject.tag == "BossDamageable")
        {
            //Debug.Log("Hit");
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);

            }
            Destroy(this.gameObject);
        }
    }
}
