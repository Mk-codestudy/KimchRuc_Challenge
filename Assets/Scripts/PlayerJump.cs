using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 15;
    Rigidbody2D rb;

    public bool isGrounded;
    Animator anim;
    BoxCollider2D BoxCollider2D;
    //int lives = 3;
    bool isUntouchable;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
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

    public void KillPlayer()
    {
        BoxCollider2D.enabled = false;
        anim.enabled = false;
        rb.AddForceY(jumpForce/2, ForceMode2D.Impulse);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isUntouchable)
            {
                Destroy(collision.gameObject);
            }
            Hit();
        }
        else if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            Heal();
        }
        else if (collision.gameObject.tag == "Super")
        {
            Destroy(collision.gameObject);
            Untouchable();
        }
    }

    void Hit()
    {
        GameManager.instance.lives--;
    }

    void Heal()
    {
        GameManager.instance.lives = Mathf.Min(3, GameManager.instance.lives + 1);
    }

    void Untouchable()
    {
        isUntouchable = true;
        Invoke("StopUntouchable", 5f);
    }
    
    void StopUntouchable()
    {
        isUntouchable = false;
    }
}
