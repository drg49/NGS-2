using UnityEngine;
using System.Collections.Generic;

public class TriggerShowInstruction : MonoBehaviour
{
    [Header("Instruction")]
    [SerializeField] private string instructionMessage = "I need to take a shower";
    [SerializeField] private float delayBeforeShow = 4f;

    [Header("Objectives (optional)")]
    [SerializeField] private bool showObjectives = false;
    [SerializeField] private List<string> objectives = new List<string>();
    [SerializeField] private int startActiveIndex = 0;

    [Header("References")]
    [SerializeField] private PauseMenuObjectivesController objectivesController;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;
        Invoke(nameof(Show), delayBeforeShow);
    }

    private void Show()
    {
        // Show instructional text
        InstructionTextAlphaFader fader = FindFirstObjectByType<InstructionTextAlphaFader>();
        if (fader != null)
            fader.Show(instructionMessage);

        // Show objectives if needed
        if (showObjectives && objectives.Count > 0)
        {
            if (objectivesController != null)
            {
                objectivesController.SetObjectives(objectives, startActiveIndex);
                objectivesController.ShowPanel(true);
            }
            else
            {
                Debug.LogError("ObjectivesController not assigned on trigger!");
            }
        }
    }
}
