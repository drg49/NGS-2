using UnityEngine;

public class DialogueTwo : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueTwoInkJSON;
    [SerializeField] private GameObject campfirePlayer;
    [SerializeField] private GameObject nightCutsceneCamTwo;

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
        Destroy(nightCutsceneCamTwo);
        campfirePlayer.SetActive(true);
        Destroy(gameObject);
    }
}
