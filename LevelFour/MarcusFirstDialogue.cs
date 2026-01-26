using UnityEngine;

public class MarcusFirstDialogue : NPCDialogue
{
    [SerializeField] private GameObject shoppingInstruction;

    private void OnEnable()
    {
        dialogueManager.OnDialogueFinished += DialogueEnded;
    }

    private void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= DialogueEnded;
    }

    private void DialogueEnded()
    {
        cameraToActivate.gameObject.SetActive(false);
        player.SetActive(true);
        shoppingInstruction.SetActive(true);
        Destroy(this);
    }
}
