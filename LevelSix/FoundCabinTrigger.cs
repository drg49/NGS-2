using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FoundCabinTrigger : MonoBehaviour
{
    [Header("Trigger Settings")]
    private bool hasTriggered = false;

    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset foundCabinDialogue;
    [SerializeField] private GameObject foundCabinCam;
    [SerializeField] private GameObject player;

    void OnEnable()
    {
        dialogueManager.OnDialogueFinished += EndDialogue;
    }

    void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= EndDialogue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (!other.CompareTag("Player")) return;

        hasTriggered = true;
        StartDialogue();
    }

    // Dialogue cutscene
    private void StartDialogue()
    {
        player.SetActive(false);
        foundCabinCam.SetActive(true);
        dialogueManager.StartStory(foundCabinDialogue);
    }

    private void EndDialogue()
    {
        Destroy(foundCabinCam);
        player.SetActive(true);
        Destroy(gameObject);
    }
}