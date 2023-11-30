using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Basic Functions")]
    [SerializeField] private GameObject playerShip;
    [SerializeField] private float moveSpeed;
    public float currentShieldAmount;
    public float maxShieldAmount;
    // Start is called before the first frame update
    void Awake()
    {
        health = this.gameObject.GetComponent<Health>();
    }
    //setup for damage

    private void Start()
    {
        maxShieldAmount = currentShieldAmount;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Shooting();
        UseBomb();
    }

    //Shooting Mechanics
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireCooldown = 0;
    [SerializeField] private AudioSource bulletSound;

    private void Shooting()
    {
        fireCooldown += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (fireCooldown >= fireRate)
            {
                Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                bulletSound.Play();
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
        }
    }


    //Damages and Kills the Player
    public Health health;
    public void TakeDamage(float damageAmount)
    {
        if (currentShieldAmount > 0)
        {
            currentShieldAmount -= damageAmount;
        }
        else
        {
            health.hitpoints--;
            health.HealthCheck();
        }

    }

    public void UseBomb()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || (Input.GetKeyDown(KeyCode.RightShift)))
        {
            GameManager._instance.UseBomb();
        }
    }

    public void Death()
    {
        GameManager._instance.LoseALife();
        this.gameObject.SetActive(false);
    }
}
