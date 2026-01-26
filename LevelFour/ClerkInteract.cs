using UnityEngine;

public class ClerkInteract : NPCDialogue
{
    [SerializeField] private GameObject shoppingListUI;
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
        Destroy(shoppingListUI);
        base.Interact();
    }

    private void DialogueEnded()
    {
        cameraToActivate.gameObject.SetActive(false);
        player.SetActive(true);
        Destroy(gameObject);
    }
}
