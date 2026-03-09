using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class RabbitSpawnArea : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject rabbitPrefab;
    public int rabbitCount = 5;

    [Header("Input System")]
    public InputActionReference interactAction; // assign your Interact action here

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

            // Assign InputAction at runtime
            RabbitAI rabbitAI = rabbit.GetComponent<RabbitAI>();
            if (rabbitAI != null)
            {
                rabbitAI.interactAction = interactAction;
            }

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