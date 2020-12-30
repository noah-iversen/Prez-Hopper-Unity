using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{
    public bool jumped = false; public bool doubleJumped = false;
    public bool canDoubleJump = false;
    public Animator animator;
    public GameObject player;

    private void Start()
    {
        Physics2D.gravity = new Vector2(0f, -80f);
    }
    private void FixedUpdate()
    {
        StartJump();
    }
    private void Update()
    {
        if (!GameSystem.Instance.gameEnded)
        {
            Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            if (jumped && velocity.y > 0 && !doubleJumped)
            {
                canDoubleJump = true;
            }
            else
            {
                canDoubleJump = false;
            }
        }
    }
    private void StartJump()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (!GameSystem.Instance.gameEnded)
            {
                Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
                if (doubleJumped || velocity.y < 0)
                {
                    return;
                }
                jumped = true; if (canDoubleJump)
                {
                    doubleJumped = true;
                }
                float height = player.transform.lossyScale.y;
                float jumpHeight = height * 0.6f;
                gameObject.GetComponent<AudioSource>().Play();
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Mathf.Sqrt(-2.0f * Physics2D.gravity.y * jumpHeight));
            }
        }
    }
}