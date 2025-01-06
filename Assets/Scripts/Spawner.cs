using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("����")]
    public float minSpawnDelay;
    public float maxSpawnDelay;
    [Header("�ǹ�������")]
    public GameObject[] gams;

    void OnEnable()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Spawn()
    {
        var randomOBJ = gams[Random.Range(0, gams.Length)];
        
        GameObject obj = Instantiate(randomOBJ, transform.position, Quaternion.identity);
        if (GameManager.instance.state == Gamestate.Playing)
        {
            Mover move = obj.GetComponent<Mover>();
            move.moveSpeed = GameManager.instance.CalculateSpeed(move.moveSpeed);
        }
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
