using UnityEngine;
using TMPro;

public class LogManager : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;
    [SerializeField] private int requiredLogs = 5;
    [SerializeField] private BoxCollider[] logColliders;

    private int currentLogs = 0;

    void Start()
    {
        UpdateUI();
        foreach (var col in logColliders)
        {
            col.enabled = true;
        }
    }

    public void AddLog()
    {
        currentLogs++;

        UpdateUI();

        if (currentLogs >= requiredLogs)
        {
            Debug.Log("All logs collected!");
            // Disable leftover colliders
            foreach (var col in logColliders)
            {
                if (col != null)
                {
                    col.enabled = false;
                }
            }
        }
    }

    private void UpdateUI()
    {
        logText.text = $"Logs: {currentLogs} / {requiredLogs}";
    }
}
