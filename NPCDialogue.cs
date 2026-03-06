using UnityEngine;

// This script has caused more problems than it solves.
// It may be a good idea to not use this script moving forward, and instead just use interactable.
// This is because many NPC Dialogues function differently, and class for them may not be needed.
public class NPCDialogue : Interactable
{
    [Header("Player")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected IndoorAmbienceZone indoorAmbience;

    [Header("Dialogue Settings")]
    [SerializeField] protected InkDialogueManager dialogueManager;
    [SerializeField] protected TextAsset inkJSON;

    [Header("Camera")]
    [SerializeField] protected Camera cameraToActivate;

    public override void Interact()
    {
        base.Interact();

        // Indoor players need special disabling
        if (indoorAmbience)
        {
            indoorAmbience.DisablePlayerIndoors();
        } else
        {
            player.SetActive(false);
        }

        cameraToActivate.gameObject.SetActive(true);

        dialogueManager.StartStory(inkJSON);        
    }
}
