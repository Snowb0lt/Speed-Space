using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hitpoints;
    public Enemy enemy;

    public void HealthCheck()
    {
        if (hitpoints <= 0)
        {
            Debug.Log(gameObject.name + "has Died");
            if (this.gameObject.GetComponent<Enemy>() != null )
            {
                enemy = this.gameObject.GetComponent<Enemy>();
                GameManager.Instance.AddScore(enemy.enemyScore);
            }
            Destroy(this.gameObject);
        }
        else return;
    }
}
