using UnityEngine;

public class DialogueFive : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueFiveInkJSON;
    [SerializeField] private GameObject midnightCutsceneCamOne;
    [SerializeField] private GameObject midnightCutsceneCamTwo;

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
        dialogueManager.StartStory(dialogueFiveInkJSON);
    }

    private void EndDialogue()
    {
        Destroy(midnightCutsceneCamOne);
        midnightCutsceneCamTwo.SetActive(true);
        Destroy(gameObject);
    }
}
