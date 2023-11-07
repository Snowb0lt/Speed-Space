using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>().GetComponent<Player>();
        color = ShieldIcon.color;
    }

    [SerializeField] private Image ShieldIcon;
    private Color color;
    // Update is called once per frame
    void Update()
    {
        UpdateShields();
    }

    private void UpdateShields()
    {
        ShieldIcon.color = color;
        color.a = player.currentShieldAmount / player.maxShieldAmount;
    }

    public TMP_Text scoretext;

}
