using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("설정")]
    public float minSpawnDelay;
    public float maxSpawnDelay;
    [Header("건물오브제")]
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
        Instantiate(randomOBJ, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
