using UnityEngine;
using TMPro;

public class RabbitManager : MonoBehaviour
{
    public static RabbitManager Instance;

    [SerializeField] private GameObject prepareRabbitsObjective;

    [Header("UI")]
    public TextMeshProUGUI rabbitText;

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

        if (collectedRabbits == totalRabbitsToCollect)
        {
            Debug.Log("Rabbits collected!");
            // All rabbits collected
            prepareRabbitsObjective.SetActive(true);
        }
    }

    private void UpdateUI()
    {
        if (rabbitText != null)
            rabbitText.text = $"Rabbits: {collectedRabbits} / {totalRabbitsToCollect}";
    }
}