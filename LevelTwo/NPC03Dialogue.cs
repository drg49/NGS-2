using UnityEngine;

public class NPC03Dialogue : NPCDialogue
{
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject instructionTwo;

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

    // Any custom behavior you want right when interaction starts
    public override void Interact()
    {
        base.Interact();
        // We only want this NPC to be interactable once
        gameObject.layer = LayerMask.NameToLayer("Default");
        Destroy(instructionTwo);
    }

    // Called automatically when the Ink dialogue finishes
    private void DialogueEnded()
    {
        // Keep reticle disabled since we are going to keep the camera active for a bit
        reticle.SetActive(false);

        // Enable NPC Path Walker
        PathWalker pathWalker = GetComponent<PathWalker>();
        pathWalker.enabled = true;
    }
}
