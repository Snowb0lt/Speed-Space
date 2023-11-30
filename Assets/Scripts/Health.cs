using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hitpoints;
    private Enemy enemy;

    public void HealthCheck()
    {
        if (hitpoints <= 0)
        {
            this.GetComponent<IDamageable>().Death();
            Debug.Log(gameObject.name + "has Died");
        }
        else return;
    }
}
