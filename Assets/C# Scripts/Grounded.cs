using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Grounded : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.Instance.gameEnded)
        {
            animator.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            animator.SetBool("Jumped", false);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Jump>().jumped = false;
            GetComponent<Jump>().canDoubleJump = false;
            GetComponent<Jump>().doubleJumped = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            animator.SetBool("Jumped", true); GetComponent<Jump>().jumped = true;
        }
    }
}