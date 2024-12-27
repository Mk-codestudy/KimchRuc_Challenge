using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("����")]
    public float minSpawnDelay;
    public float maxSpawnDelay;
    [Header("�ǹ�������")]
    public GameObject[] gams;

    void Start()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void Spawn()
    {
        var randomOBJ = gams[Random.Range(0, gams.Length)];
        Instantiate(randomOBJ, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
