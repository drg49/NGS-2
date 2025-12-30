using UnityEngine;

public class TriggerShowInstruction : MonoBehaviour
{
    [SerializeField] private InstructionSequence instructionSequence;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;

        if (instructionSequence != null)
        {
            instructionSequence.Play();
        }
        else
        {
            Debug.LogError("InstructionSequence not assigned on TriggerShowInstruction!");
        }
    }
}
