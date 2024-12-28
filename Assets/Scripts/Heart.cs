using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite onHeart;
    public Sprite offHeart;

    public SpriteRenderer SpriteRenderer;

    public int Heartnum;

    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameManager.instance.lives >= Heartnum)
        {
            SpriteRenderer.sprite = onHeart;
        }
        else
        {
            SpriteRenderer.sprite=offHeart;
        }
    }
}
