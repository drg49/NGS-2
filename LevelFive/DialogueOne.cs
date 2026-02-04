using UnityEngine;

public class DialogueOne : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueOneInkJSON;
    [SerializeField] private GameObject dialogueOneCam;
    [SerializeField] private GameObject player;
    [SerializeField] private PathWalker marcusPathWalker;
    [SerializeField] private PathWalker davidPathWalker;
    [SerializeField] private GameObject tentObjective;

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
        dialogueManager.StartStory(dialogueOneInkJSON);
    }

    private void EndDialogue()
    {
        Destroy(dialogueOneCam);
        player.SetActive(true);
        marcusPathWalker.enabled = true;
        davidPathWalker.enabled = true;
        tentObjective.SetActive(true);
        Destroy(gameObject);
    }
}
