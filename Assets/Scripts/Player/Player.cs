using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Basic Functions")]
    [SerializeField] private GameObject playerShip;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int shieldAmount;
    // Start is called before the first frame update
    void Awake()
    {
        health = this.gameObject.GetComponent<Health>();
    }

    private void Start()
    {
        health.hitpoints = 1 + shieldAmount;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        Shooting();
    }

    //Shooting Mechanics
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireCooldown = 0;

    private void Shooting()
    {
        fireCooldown += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (fireCooldown >= fireRate)
            {
                Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                fireCooldown = 0;
            }
        }
    }

    //Controls how the player moves
    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerShip.transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerShip.transform.position = transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerShip.transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            playerShip.transform.position = transform.position + (Vector3.up * moveSpeed) * Time.deltaTime;
            BackgroundScroll.instance.scrollSpeed = 15;
        }
        else
        {
            BackgroundScroll.instance.scrollSpeed = 10;
        }
    }


    //Damages and Kills the Player
    private Health health;
    public void TakeDamage(float damageAmount)
    {
        health.hitpoints--;
        health.HealthCheck();
    }
}
