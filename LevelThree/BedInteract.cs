using TMPro;
using UnityEngine;

public class BedInteract : Interactable
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource gettingInBedAudio;

    public override void Interact()
    {
        fadeAnimator.SetTrigger("FadeIntoBed");
        FirstPersonController controller = player.GetComponent<FirstPersonController>();
        controller.enabled = false;
        gettingInBedAudio.Play();
        Destroy(gameObject);
    }
}
