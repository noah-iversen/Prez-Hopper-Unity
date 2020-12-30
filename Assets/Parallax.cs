using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Parallax : MonoBehaviour
{
    public float speed;
    public bool duplicate;
    private float length;
    public GameObject cam;
    private Vector3 startpos;

    private void Start()
    {
        transform.position = new Vector3(0, 1, 0); length = GetComponent<SpriteRenderer>().bounds.size.x;
        if (duplicate)
        {
            startpos = new Vector3(length + transform.position.x, 1, 0);
            transform.position = startpos;
        }
    }
    private void Update()
    {
        if (GameSystem.Instance.gameEnded == false)
        {
            if (transform.position.x <= -length)
            {
                transform.position = new Vector2(length + (transform.position.x + length), transform.position.y);
            }
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}