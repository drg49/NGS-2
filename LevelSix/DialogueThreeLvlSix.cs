using UnityEngine;

public class DialogueThreeLvlSix : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueThreeInkJSON;

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
        dialogueManager.StartStory(dialogueThreeInkJSON);
    }

    private void EndDialogue()
    {
        Destroy(gameObject);
    }
}
