using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private Vector2 screenBoundaries;
    private float halfPlayerWidth;

    private void Start()
    {
        halfPlayerWidth = transform.localScale.x / -2;
        screenBoundaries =  new Vector2(Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth, Camera.main.orthographicSize);
    }
    private void LateUpdate()
    {
        if (transform.position.x < -screenBoundaries.x)
        {
            transform.position = new Vector2(-screenBoundaries.x, transform.position.y);
        }       
        else if (transform.position.x > screenBoundaries.x)
        {
            transform.position = new Vector2(screenBoundaries.x, transform.position.y);
        }
        if (transform.position.y < -screenBoundaries.y)
        {
            transform.position = new Vector2(transform.position.x, -screenBoundaries.y);
        }
        else if (transform.position.y > screenBoundaries.y)
        {
            transform.position = new Vector2(transform.position.x, screenBoundaries.y);
        }
    }
}
