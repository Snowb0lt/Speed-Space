using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShipTurret : MonoBehaviour, IDamageable
{
    private GunShipBoss boss;
    private Health health;
    private void Awake()
    {
        boss = GameObject.FindAnyObjectByType<GunShipBoss>().GetComponent<GunShipBoss>();
        health = GetComponent<Health>();
    }
    public void Death()
    {
        GameManager._instance.AddScore(100);
        boss.Turrets.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damageAmount)
    {
        health.hitpoints = health.hitpoints - damageAmount;
        health.HealthCheck();
    }


}
