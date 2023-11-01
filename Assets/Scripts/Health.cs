using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hitpoints;

    public void Death()
    {
        if (hitpoints <= 0)
        {
            Debug.Log(gameObject.name + "has Died");
            Destroy(this.gameObject);
        }
    }
}
