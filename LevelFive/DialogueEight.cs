using UnityEngine;

public class DialogueEight : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueEightInkJSON;
    [SerializeField] private Animator fadeAnim;

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
        dialogueManager.StartStory(dialogueEightInkJSON);
    }

    private void EndDialogue()
    {
        Destroy(gameObject);
    }
}
