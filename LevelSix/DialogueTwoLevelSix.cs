using UnityEngine;

public class DialogueTwoLevelSix : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueTwoInkJSON;
    [SerializeField] private BoxCollider[] collidersToActivate;

    void OnEnable()
    {
        dialogueManager.OnDialogueFinished += EndDialogue;
    }

    void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= EndDialogue;
    }

    void Start()
    {
        dialogueManager.StartStory(dialogueTwoInkJSON);
    }

    private void EndDialogue()
    {
        // Player can start eating
        foreach (BoxCollider collider in collidersToActivate)
        {
            collider.enabled = true;
        }
        Destroy(gameObject);
    }
}
