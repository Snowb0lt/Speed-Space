using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private List<Animator> animations = new List<Animator>();
    public void ActivateBomb()
    {
        foreach (var anim in animations)
        {
            anim.SetTrigger("PlayTrigger");
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject hostiles in enemies)
            Destroy(hostiles.gameObject);
    }
}
