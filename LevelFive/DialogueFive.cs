using UnityEngine;
using UnityEngine.UI;

public class DialogueFive : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueFiveInkJSON;
    [SerializeField] private GameObject midnightCutsceneCamOne;
    [SerializeField] private GameObject midnightCutsceneCamTwo;
    [SerializeField] private GameObject davidWaypointTwo;
    [SerializeField] private PathWalker davidPathWalker;
    [SerializeField] private Animator davidAnim;
    [SerializeField] private Image reticleImage;

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
        // Make reticle invisible throughout cutscenes
        reticleImage.enabled = false;

        Destroy(midnightCutsceneCamOne);
        midnightCutsceneCamTwo.SetActive(true);
        // David walks over to look into the forest
        davidWaypointTwo.SetActive(true);
        // Refresh David's Pathwalker
        davidPathWalker.enabled = false;
        davidPathWalker.enabled = true;
        davidPathWalker.SetMoveSpeed(1.2f);
        davidAnim.SetTrigger("WalkToWoods");
        Destroy(gameObject);
    }
}
