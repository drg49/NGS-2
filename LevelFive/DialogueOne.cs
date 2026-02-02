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

    void Start()
    {
        dialogueManager.OnDialogueFinished = EndDialogue;
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
