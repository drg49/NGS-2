using UnityEngine;
using System.Collections.Generic;

public class RabbitSpawnArea : MonoBehaviour
{
    public GameObject rabbitPrefab;
    public int rabbitCount = 5;

    private BoxCollider area;
    private readonly List<GameObject> rabbits = new();

    void Awake()
    {
        area = GetComponent<BoxCollider>();
    }

    void Start()
    {
        SpawnRabbits();
    }

    void SpawnRabbits()
    {
        for (int i = 0; i < rabbitCount; i++)
        {
            Vector3 spawnPos = GetRandomPointInBounds();

            GameObject rabbit = Instantiate(rabbitPrefab, spawnPos, Quaternion.identity);

            rabbits.Add(rabbit);
        }
    }

    Vector3 GetRandomPointInBounds()
    {
        Vector3 center = area.bounds.center;
        Vector3 size = area.bounds.size;

        return new Vector3(
            Random.Range(center.x - size.x / 2, center.x + size.x / 2),
            center.y,
            Random.Range(center.z - size.z / 2, center.z + size.z / 2)
        );
    }
}
