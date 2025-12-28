using UnityEngine;

public class CellphoneInteract : Interactable
{
    [SerializeField] private GameObject marcusPhoneCall;
    [SerializeField] private GameObject ringtone;
    [SerializeField] private AudioSource phonePickup;
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset marcusInkJSON;

    public override void Interact()
    {
        Debug.Log("Phone Answered");
        ringtone.SetActive(false);
        phonePickup.Play();
        marcusPhoneCall.SetActive(true);
        dialogueManager.OnDialogueFinished = OnPhoneCallFinished;
        // Start the Ink dialogue
        dialogueManager.StartStory(marcusInkJSON);

        var renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;

        var collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    private void OnPhoneCallFinished()
    {
        // Anything you want:
        // play animation
        // enable new interactables
        // trigger jump scare
        Debug.Log("Phone call ended");
    }
}
