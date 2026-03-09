using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class RabbitSpawnArea : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject rabbitPrefab;
    public int rabbitCount = 8;

    [Header("Spawn Zones")]
    public BoxCollider[] spawnZones; // Multiple box colliders in the scene

    [Header("Input System")]
    public InputActionReference interactAction; // Assign your Interact action

    private readonly List<GameObject> rabbits = new();

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

            // Assign input action at runtime
            RabbitAI rabbitAI = rabbit.GetComponent<RabbitAI>();
            if (rabbitAI != null)
            {
                rabbitAI.interactAction = interactAction;
            }

            rabbits.Add(rabbit);
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