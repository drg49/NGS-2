using TMPro;
using UnityEngine;

public class DialogueThree : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueThreeInkJSON;
    [SerializeField] private Animator fadeAnimator;

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
        fadeAnimator.SetTrigger("LeaveCampfire");
        Destroy(gameObject);
    }
}