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
        // Start the Ink dialogue
        dialogueManager.StartStory(marcusInkJSON);

        gameObject.SetActive(false);
    }
}
