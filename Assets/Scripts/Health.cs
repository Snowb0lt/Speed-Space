using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hitpoints;

    public void HealthCheck()
    {
        if (hitpoints <= 0)
        {
            Debug.Log(gameObject.name + "has Died");
            Destroy(this.gameObject);
        }
        else return;
    }
}
