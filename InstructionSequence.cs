using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstructionSequence : MonoBehaviour
{
    [Header("Instruction")]
    [SerializeField] private string instructionMessage = "";
    [SerializeField] private float delayBeforeShow = 4f;

    [Header("Objectives (optional)")]
    [SerializeField] private bool showObjectives = false;
    [SerializeField] private List<string> objectives = new List<string>();
    [SerializeField] private int startActiveIndex = 0;

    [Header("References")]
    [SerializeField] private PauseMenuObjectivesController objectivesController;

    private bool hasPlayed = false;

    /// <summary>
    /// Call this from ANY script to trigger the sequence.
    /// </summary>
    public void Play()
    {
        if (hasPlayed) return;
        hasPlayed = true;

        // Set objectives immediately
        if (showObjectives && objectives.Count > 0)
        {
            if (objectivesController != null)
            {
                objectivesController.SetObjectives(objectives, startActiveIndex);
                objectivesController.ShowPanel(true);
            }
            else
            {
                Debug.LogError("ObjectivesController not assigned!");
            }
        }

        // Delay instruction text only
        StartCoroutine(ShowInstructionDelayed());
    }

    private IEnumerator ShowInstructionDelayed()
    {
        yield return new WaitForSeconds(delayBeforeShow);

        InstructionTextAlphaFader fader = FindFirstObjectByType<InstructionTextAlphaFader>();
        if (fader != null)
        {
            fader.Show(instructionMessage);
        }
    }

    /// <summary>
    /// Optional: allows replaying if needed.
    /// </summary>
    public void ResetSequence()
    {
        hasPlayed = false;
    }
}
