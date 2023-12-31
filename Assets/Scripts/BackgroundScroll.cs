using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField]private Sprite _sprite;
    public float scrollSpeed;
    private Vector3 startPos;
    private float repeatHeight;

    public static BackgroundScroll instance;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }
    private void Start()
    {
        _sprite = GetComponent<Sprite>();
        startPos = transform.position;
        repeatHeight = GetComponent<BoxCollider2D>().size.y/2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * scrollSpeed);
        if (transform.position.y < startPos.y - repeatHeight)
        {
            transform.position = startPos;
        }
    }
}
