using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMine : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damageAmount)
    {
        throw new System.NotImplementedException();
    }
    [SerializeField] private GameObject MineBody;
    [SerializeField] private GameObject Boom;
    private void Detonate()
    {
        MineBody.SetActive(false);
        Boom.SetActive(true);
        Boom.transform.localScale += new Vector3(1, 0, 1);

        Destroy(this.gameObject, 4);
    }
}
