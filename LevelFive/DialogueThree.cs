using TMPro;
using UnityEngine;

public class DialogueThree : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueThreeInkJSON;
    [SerializeField] private GameObject campfirePlayer;

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