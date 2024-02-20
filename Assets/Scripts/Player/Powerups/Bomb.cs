using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Bomb : MonoBehaviour
{
    [SerializeField] private List<Animator> animations = new List<Animator>();
    [SerializeField] private GameObject Nuke, nukeStart;
    [SerializeField] private float DetTime;
    public void ActivateBomb()
    {
        nukeStart.SetActive(true);
        foreach (var anim in animations)
        {
            anim.SetTrigger("PlayTrigger");
        }
        Invoke("DetonateNuke", DetTime);
    }

    private void DetonateNuke()
    {
        nukeStart.SetActive(false);
        Instantiate(Nuke, transform.position, Quaternion.identity);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject hostiles in enemies)
        {
            Destroy(hostiles.gameObject);
        }
    }
}
