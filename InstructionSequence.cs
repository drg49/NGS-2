using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstructionSequence : MonoBehaviour
{
    [Header("Instruction")]
    [SerializeField] private string instructionMessage = "";
    [SerializeField] private float delayBeforeShow = 4f;
    [Tooltip("How long the instruction stays visible (-1 = infinite)")]
    [SerializeField] private float visibleDuration = 5f;

    [Header("Objectives (optional)")]
    [SerializeField] private bool showObjectives = false;
    [SerializeField] private List<string> objectives = new List<string>();
    [SerializeField] private int startActiveIndex = 0;

    [Header("References")]
    [SerializeField] private PauseMenuObjectivesController objectivesController;

    [Header("Optional Activation")]
    [Tooltip("GameObjects to activate when the instruction shows.")]
    [SerializeField] private GameObject[] optionalGameObjectsToActivate;

    [Header("Play Settings")]
    [SerializeField] private bool playOnAwake = false;

    private bool hasPlayed = false;

    private void Awake()
    {
        if (playOnAwake)
        {
            Play();
        }
    }

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

        StartCoroutine(ShowInstructionDelayed());
    }

    private IEnumerator ShowInstructionDelayed()
    {
        yield return new WaitForSeconds(delayBeforeShow);

        InstructionTextAlphaFader fader = FindFirstObjectByType<InstructionTextAlphaFader>();
        if (fader != null)
        {
            fader.Show(instructionMessage, visibleDuration);
        }
        else
        {
            Debug.LogWarning("InstructionTextAlphaFader not found in scene.");
        }

        // Activate optional GameObjects
        if (optionalGameObjectsToActivate != null)
        {
            foreach (GameObject go in optionalGameObjectsToActivate)
            {
                if (go != null)
                    go.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Allows replaying if needed.
    /// </summary>
    public void ResetSequence()
    {
        hasPlayed = false;
    }
}
