using UnityEngine;

public class NPC03Dialogue : NPCDialogue
{
    private void OnEnable()
    {
        if (dialogueManager != null)
            dialogueManager.OnDialogueFinished += DialogueEnded;
    }

    private void OnDisable()
    {
        if (dialogueManager != null)
            dialogueManager.OnDialogueFinished -= DialogueEnded;
    }

    public override void Interact()
    {
        base.Interact();
        // Any custom behavior you want right when interaction starts
    }

    private void DialogueEnded()
    {
        // Called automatically when the Ink dialogue finishes
        Debug.Log($"NPC03 Dialogue ended!");

        // You can also trigger custom events, give rewards, etc.
    }
}
