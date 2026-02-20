using UnityEngine;

public class DialogueSix : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueSixInkJSON;
    [SerializeField] private GameObject davidWaypointOne;
    [SerializeField] private GameObject davidWaypointTwo;
    [SerializeField] private GameObject davidWaypointThree;
    [SerializeField] private PathWalker davidPathWalker;
    [SerializeField] private Animator davidAnim;
    [SerializeField] private GameObject marcus;
    [SerializeField] private Animator marcusAnim;
    [SerializeField] private Transform marcusSleepTarget;

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
        dialogueManager.StartStory(dialogueSixInkJSON);
    }

    private void EndDialogue()
    {
        // Disable previous waypoints
        davidWaypointOne.SetActive(false);
        davidWaypointTwo.SetActive(false);
        // David runs into the forest
        davidWaypointThree.SetActive(true);
        // Refresh David's Pathwalker
        davidPathWalker.enabled = false;
        davidPathWalker.enabled = true;
        davidPathWalker.SetMoveSpeed(7f);
        davidAnim.SetTrigger("RunIntoWoods");
        marcus.SetActive(true);
        // Set Marcus NPC in tent
        marcus.transform.SetPositionAndRotation(
            marcusSleepTarget.position,
            marcusSleepTarget.rotation
        );
        marcusAnim.SetTrigger("SleepInTent");
        Destroy(gameObject);
    }
}
