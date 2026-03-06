using UnityEngine;

public class HelpMarcusInteract : Interactable
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset helpMarcusDialogue;
    [SerializeField] private GameObject helpMarcusCam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerNPC;
    [SerializeField] private AudioSource aftermathSong;
    [SerializeField] private FirstPersonController fpsController;

    void OnEnable()
    {
        dialogueManager.OnDialogueFinished += EndDialogue;
    }

    void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= EndDialogue;
    }

    public override void Interact()
    {
        player.SetActive(false);
        playerNPC.SetActive(true);
        helpMarcusCam.SetActive(true);
        dialogueManager.StartStory(helpMarcusDialogue);
        aftermathSong.Play();
    }

    private void EndDialogue()
    {
        Destroy(helpMarcusCam);
        playerNPC.SetActive(false);
        player.SetActive(true);
        // Player cannot run at the start of this level
        // When the player begins to look for help they can run
        fpsController.canRun = true;
        Destroy(gameObject);
    }
}
