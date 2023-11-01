using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector2 travelPoint;
    [SerializeField] private Vector2 selectionBounds;

    private void Awake()
    {

    }
    private void Start()
    {
        selectionBounds = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        PickTravelLocation();

    }
    //Pick Location to Travel to
    public void PickTravelLocation()
    {
        float positionY = Random.Range(-selectionBounds.y, selectionBounds.y);
        float positionX = Random.Range(-selectionBounds.x, selectionBounds.x);
        travelPoint = new Vector2(positionX, positionY);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PickTravelLocation();
        }
        transform.position = Vector2.Lerp(transform.position, travelPoint, 5 * Time.deltaTime);
    }  
}
