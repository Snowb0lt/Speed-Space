using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public void ActivateBomb()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject hostiles in enemies)
            hostiles.GetComponent<Enemy>().TakeDamage(5);
    }
}
