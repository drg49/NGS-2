using UnityEngine;
using UnityEngine.UI;

public class WakeUpMarcusInteract : Interactable
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueSevenInkJSON;
    [SerializeField] private GameObject wakeUpMarcusCam;
    [SerializeField] private GameObject davidSleepInTentCam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject marcusTentFlashlight;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject flashlightAudio;
    [SerializeField] private GameObject flashlightLight;
    [SerializeField] private GameObject david;
    [SerializeField] private PathWalker davidPathWalker;
    [SerializeField] private Transform davidSleepTarget;
    [SerializeField] private Animator davidAnim;
    [SerializeField] private Animator marcusAnim;
    [SerializeField] private Image reticleImage;

    void OnEnable()
    {
        dialogueManager.OnDialogueFinished += EndDialogue;
    }

    void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= EndDialogue;
    }

    public override void Interact()
    {
        player.SetActive(false);
        wakeUpMarcusCam.SetActive(true);
        marcusAnim.SetTrigger("WakeUp");
        dialogueManager.StartStory(dialogueSevenInkJSON);
        // Disable only the box collider for this interactable object
        GetComponent<BoxCollider>().enabled = false;
        marcusTentFlashlight.SetActive(true);
        Destroy(flashlight);
        Destroy(flashlightAudio);
        Destroy(flashlightLight);
        // Disable David's path walker and set him in the tent
        davidPathWalker.enabled = false;
        david.SetActive(true);
        david.transform.SetPositionAndRotation(
            davidSleepTarget.position,
            davidSleepTarget.rotation
        );
        davidAnim.SetTrigger("SleepInTent");
    }

    private void EndDialogue()
    {
        // Make reticle invisible throughout cutscenes
        reticleImage.enabled = false;
        // Switch cameras
        wakeUpMarcusCam.SetActive(false);
        davidSleepInTentCam.SetActive(true);
        Destroy(gameObject);
    }
}
