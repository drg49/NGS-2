using System.Collections;
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
        StartCoroutine(Wait());
    }

    private void EndDialogue()
    {
        Destroy(gameObject);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(6f);
        dialogueManager.StartStory(dialogueThreeInkJSON);
    }
}
