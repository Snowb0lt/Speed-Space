using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Bounds TravelArea;
    private Vector3 travelPoint;

    private void Awake()
    {
        
    }
    private void Start()
    {
        PickTravelLocation();
    }
    //Pick Location to Travel to
    public void PickTravelLocation()
    {
        travelPoint = new Vector3(
            Random.Range(TravelArea.min.x, TravelArea.max.x),
            Random.Range(TravelArea.min.y, TravelArea.max.y),
            transform.position.z);
    }
    private void Update()
    {
        transform.position = transform.position + Vector3.MoveTowards(transform.position, travelPoint, 0) * Time.deltaTime;
    }
}
