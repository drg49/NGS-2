using UnityEngine;

public class DavidInteract : NPCDialogue
{
    [SerializeField] private MarcusFirstDialogue marcusFirstDialogue;
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
        if (marcusFirstDialogue != null)
        {
            marcusFirstDialogue.enabled = false;
        }
        base.Interact();
    }

    private void DialogueEnded()
    {
        cameraToActivate.gameObject.SetActive(false);
        player.SetActive(true);
        if (marcusFirstDialogue != null)
        {
            marcusFirstDialogue.enabled = true;
        }
    }
}
