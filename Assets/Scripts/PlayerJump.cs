using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    public AudioClip[] sound;
    AudioSource audioSource;

    SpriteRenderer spriteRenderer;
    CancellationTokenSource cts;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            //rb.linearVelocityX = 10;
            //rb.linearVelocityY = 20;
            if (isGrounded)
            {
                rb.AddForceY(jumpForce, ForceMode2D.Impulse);
                anim.SetInteger("State", 1);
                audioSource.PlayOneShot(sound[0]);
                isGrounded = false;
            }
        }
    }

    public void KillPlayer()
    {
        BoxCollider2D.enabled = false;
        anim.enabled = false;
        rb.AddForceY(jumpForce/2, ForceMode2D.Impulse);
        SoundManager.instance.DieSound();
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
                audioSource.PlayOneShot(sound[1]);
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

        if (isUntouchable && cts == null)
        {
            // ���� �����̰� ���� ���� ���� �۾��� ������ ���ο� ������ ȿ�� ����
            cts = new CancellationTokenSource();
            RainbowColor(cts.Token).Forget();
        }
        else if (!isUntouchable && cts != null)
        {
            // ���� ���°� �ƴϰ� ���� ���� �۾��� ������ �۾� ��� �� ����
            cts.Cancel();
            cts.Dispose();
            cts = null;
            spriteRenderer.color = Color.white; // ���� �������� ����
        }

    }
    async UniTask RainbowColor(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // HSV ���� ���� ����Ͽ� ������ ���� ����
            float h = (Mathf.Sin(Time.time * 3f) + 1f) * 0.5f;
            spriteRenderer.color = Color.HSVToRGB(h, 1f, 1f);

            // ���� �����ӱ��� ��� (UniTask�� Yield ���)
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
        }
    }

    void OnDisable()
    {
        // ������Ʈ�� ��Ȱ��ȭ�� �� ���� ���� �۾� ��� �� ����
        cts?.Cancel();
        cts?.Dispose();
    }

    void Hit()
    {
        GameManager.instance.lives--;
    }

    void Heal()
    {
        GameManager.instance.lives = Mathf.Min(3, GameManager.instance.lives + 1);
        SoundManager.instance.EatSound();
    }

    void Untouchable()
    {
        isUntouchable = true;
        Invoke("StopUntouchable", 5f);
    }
    
    void StopUntouchable()
    {
        isUntouchable = false;
        SoundManager.instance.EatSound();
    }
}
