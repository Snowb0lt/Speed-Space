using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private int bulletSpeed = 20;
    private float bulletLifespan = 10;
    [SerializeField] private float lifespanCounter;
    [SerializeField] private float damageAmount;
    private GameObject target;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindWithTag("Player");
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.AddForce(target.transform.position - transform.position, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        BulletMechanics();
    }

    private void BulletMechanics()
    {
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
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
                Destroy(this.gameObject);
            }

        }
    }
}
