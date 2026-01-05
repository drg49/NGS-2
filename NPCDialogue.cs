using UnityEngine;

public class NPCDialogue : Interactable
{
    [Header("Player")]
    [SerializeField] protected GameObject player;

    [Header("Dialogue Settings")]
    [SerializeField] protected InkDialogueManager dialogueManager;
    [SerializeField] protected TextAsset inkJSON;

    [Header("Camera")]
    [SerializeField] protected Camera cameraToActivate;

    public override void Interact()
    {
        base.Interact();

        player.SetActive(false);
        cameraToActivate.gameObject.SetActive(true);

        dialogueManager.StartStory(inkJSON);        
    }
}
