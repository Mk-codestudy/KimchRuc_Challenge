using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce;
    Rigidbody2D rb;

    public bool isGrounded;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //rb.linearVelocityX = 10;
            //rb.linearVelocityY = 20;

            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            anim.SetInteger("State", 1);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "platform")
        {
            if (!isGrounded)
            {
                anim.SetInteger("State", 2);
            }
            isGrounded = true;
        }

    }

}
