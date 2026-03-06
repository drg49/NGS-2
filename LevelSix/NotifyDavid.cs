using UnityEngine;

public class NotifyDavid : NPCDialogue
{
    [SerializeField] private Transform davidTransform;

    private void OnEnable()
    {
        davidTransform.rotation = Quaternion.Euler(0f, 90f, 0f);
        dialogueManager.OnDialogueFinished += DialogueEnded;
    }

    private void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= DialogueEnded;
    }

    private void DialogueEnded()
    {
        Destroy(cameraToActivate.gameObject);
        player.SetActive(true);
        Destroy(gameObject);
    }
}