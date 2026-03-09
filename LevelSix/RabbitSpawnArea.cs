using UnityEngine;
using UnityEngine.InputSystem;

public class RabbitSpawnArea : MonoBehaviour
{
    public GameObject rabbitPrefab;
    public int rabbitCount = 8;

    public InputActionReference interactAction;

    private BoxCollider[] spawnZones;

    void Awake()
    {
        spawnZones = GetComponents<BoxCollider>();
    }

    void Start()
    {
        SpawnRabbits();
    }

    void SpawnRabbits()
    {
        for (int i = 0; i < rabbitCount; i++)
        {
            BoxCollider zone = spawnZones[Random.Range(0, spawnZones.Length)];
            Vector3 spawnPos = GetRandomPointInBounds(zone);

            GameObject rabbit = Instantiate(rabbitPrefab, spawnPos, Quaternion.identity);

            rabbit.GetComponent<RabbitAI>().interactAction = interactAction;
        }
    }

    Vector3 GetRandomPointInBounds(BoxCollider zone)
    {
        Vector3 center = zone.bounds.center;
        Vector3 size = zone.bounds.size;

        return new Vector3(
            Random.Range(center.x - size.x / 2, center.x + size.x / 2),
            center.y,
            Random.Range(center.z - size.z / 2, center.z + size.z / 2)
        );
    }
}