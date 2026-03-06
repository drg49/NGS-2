using UnityEngine;

public class HelpMarcusInteract : Interactable
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset helpMarcusDialogue;
    [SerializeField] private GameObject wakeUpMarcusCam;

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
        dialogueManager.StartStory(helpMarcusDialogue);
    }

    private void EndDialogue()
    {
        
        Destroy(gameObject);
    }
}
