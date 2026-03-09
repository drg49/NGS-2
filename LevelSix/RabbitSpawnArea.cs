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

            RabbitAI ai = rabbit.GetComponent<RabbitAI>();
            ai.interactAction = interactAction;
            ai.spawnZone = zone;
        }
    }

    Vector3 GetRandomPointInBounds(BoxCollider zone)
    {
        Bounds bounds = zone.bounds;

        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.center.y,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}