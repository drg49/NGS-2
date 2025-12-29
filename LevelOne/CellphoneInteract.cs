using System.Collections;
using TMPro;
using UnityEngine;
using static FadePanel;

public class CellphoneInteract : Interactable
{
    [SerializeField] private GameObject marcusPhoneCall;
    [SerializeField] private GameObject ringtone;
    [SerializeField] private GameObject cutsceneTrigger;
    [SerializeField] private AudioSource phonePickup;
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset marcusInkJSON;
    [SerializeField] private GameObject cellphoneInteractionText;
    [SerializeField] private float interactionTextDuration = 10f;
    [SerializeField] private Animator fadeAnimator;


    private IEnumerator ShowInteractionText()
    {
        // Short delay before text displays
        yield return new WaitForSeconds(2.7f);
        cellphoneInteractionText.SetActive(true);
        yield return new WaitForSeconds(interactionTextDuration);
        Destroy(cellphoneInteractionText);
    }


    public override void Interact()
    {
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

        // Show instructional text
        StartCoroutine(ShowInteractionText());
    }

    private void OnPhoneCallFinished()
    {
        cutsceneTrigger.SetActive(true);
        phonePickup.Play();
        marcusPhoneCall.SetActive(false);
        fadeAnimator.SetTrigger("FadeInOutPostBath");
    }
}
