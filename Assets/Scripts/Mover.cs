using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveSpeed;


    void Update()
    {
        transform.position += Vector3.left * GameManager.instance.CalculateSpeed(moveSpeed) * Time.deltaTime;
    }
}
