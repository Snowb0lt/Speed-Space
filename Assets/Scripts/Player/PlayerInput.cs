using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameObject playerShip;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        if (Input.GetKey(KeyCode.LeftShift))
        {

        }
    }

    //Controls how the player moves
    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerShip.transform.position = transform.position + Vector3.left * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerShip.transform.position = transform.position + Vector3.right * moveSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerShip.transform.position = transform.position + Vector3.down * moveSpeed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            playerShip.transform.position = transform.position + Vector3.up * moveSpeed;
        }
    }
}
