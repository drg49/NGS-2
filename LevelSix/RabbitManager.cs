using UnityEngine;
using TMPro;

public class RabbitManager : MonoBehaviour
{
    public static RabbitManager Instance;

    [Header("UI")]
    public TextMeshProUGUI rabbitText; // Assign your UI element

    [Header("Goal")]
    public int totalRabbitsToCollect = 8;

    private int collectedRabbits = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void CollectRabbit()
    {
        collectedRabbits++;
        UpdateUI();

        if (collectedRabbits >= totalRabbitsToCollect)
        {
            Debug.Log("All rabbits collected!");
            // Add win logic here
        }
    }

    private void UpdateUI()
    {
        if (rabbitText != null)
            rabbitText.text = $"Rabbits: {collectedRabbits} / {totalRabbitsToCollect}";
    }
}