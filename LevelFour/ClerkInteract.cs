using UnityEngine;

public class ClerkInteract : NPCDialogue
{
    [SerializeField] private GameObject shoppingListUI;
    [SerializeField] private GameObject carInstruction;
    [SerializeField] private AudioSource cashRegister;

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
        indoorAmbience.EnablePlayerIndoors();
        carInstruction.SetActive(true);
        cashRegister.Play();
        Destroy(gameObject);
    }
}
