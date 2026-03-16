using UnityEngine;
using System.Collections;

public class DavidJumpscareTrigger : MonoBehaviour
{
    [SerializeField] private GameObject david;
    [SerializeField] private Transform davidJumpscareTarget;
    [SerializeField] private GameObject davidJumpscareCam;
    [SerializeField] private GameObject player;
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueFourInkJSON;
    [SerializeField] private GameObject digitalBarkSong;
    [SerializeField] private AudioSource jumpscareAudio;

    private Collider triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }

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
        // Disable trigger
        triggerCollider.enabled = false;

        david.transform.SetPositionAndRotation(
            davidJumpscareTarget.position,
            davidJumpscareTarget.rotation
        );

        Destroy(digitalBarkSong);
        jumpscareAudio.Play();

        player.SetActive(false);
        davidJumpscareCam.SetActive(true);

        // Start delayed action
        StartCoroutine(DelayDialogue());
    }

    private IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(2f);
        dialogueManager.StartStory(dialogueFourInkJSON);
    }

    private void EndDialogue()
    {
        Destroy(davidJumpscareCam);
        player.SetActive(true);
        Destroy(gameObject);
    }
}