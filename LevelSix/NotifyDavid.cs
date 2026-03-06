using UnityEngine;
using UnityEngine.UI;

public class NotifyDavid : NPCDialogue
{
    [SerializeField] private Transform davidTransform;
    [SerializeField] private GameObject notifyFriendsTrigger;
    [SerializeField] private Animator fadeAnim;
    [SerializeField] private Image reticleImage;

    private void OnEnable()
    {
        davidTransform.rotation = Quaternion.Euler(0f, 90f, 0f);
        dialogueManager.OnDialogueFinished += DialogueEnded;
    }

    private void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= DialogueEnded;
    }

    private void DialogueEnded()
    {
        // Make reticle invisible throughout cutscenes
        reticleImage.enabled = false;

        // Transition to cutscene - David & Player carry Marcus into the cabin
        fadeAnim.SetTrigger("FadeInOutCarryMarcus");
        Destroy(notifyFriendsTrigger);
        Destroy(gameObject);
    }
}