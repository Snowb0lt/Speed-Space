using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMine : MonoBehaviour, IDamageable
{
    [SerializeField] private int DetonationTime;
    private float CountUp;

    private void Awake()
    {
        CountUp = 0;
    }
    private void Update()
    {

        CountUp += Time.deltaTime;
        MineBody.transform.Rotate(0, 0, CountUp);
        if (CountUp >= DetonationTime)
        {
            Detonate();
        }
    }
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
        Boom.transform.localScale += new Vector3(2, 2, 2)* Time.deltaTime *2;

        Destroy(this.gameObject, 1);
    }
}
