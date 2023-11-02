using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject bullet;
    [SerializeField] private int bulletSpeed = 20;
    private float bulletLifespan = 10;
    [SerializeField] private float lifespanCounter;
    [SerializeField] private float damageAmount;
    private GameObject target;
    // Start is called before the first frame update
    void Awake()
    {
        bullet = this.gameObject;
        target = GameObject.FindWithTag("Player");
    }
    private void Start()
    {
        BulletMechanics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BulletMechanics()
    {
        bullet.transform.position = bullet.transform.position + (Vector3.MoveTowards(transform.position, target.transform.position, bulletSpeed * Time.deltaTime));
        lifespanCounter += Time.deltaTime;
        if (lifespanCounter >= bulletLifespan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
        }

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
            Destroy(this.gameObject);
        }
        
    }
}
