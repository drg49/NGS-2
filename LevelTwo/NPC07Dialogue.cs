using UnityEngine;

public class NPC07Dialogue : NPCDialogue
{
    [SerializeField] private GameObject reticle;

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
        indoorAmbience.EnablePlayerIndoors();
        reticle.SetActive(true);
    }
}
