using UnityEngine;

public class MarcusDialogue : NPCDialogue
{
    private void OnEnable()
    {
        dialogueManager.OnDialogueFinished += DialogueEnded;
    }

    private void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= DialogueEnded;
    }

    public override void Interact()
    {
        base.Interact();
    }

    private void DialogueEnded()
    {
        Destroy(cameraToActivate.gameObject);
        player.SetActive(true);
        Destroy(gameObject);
    }
}
