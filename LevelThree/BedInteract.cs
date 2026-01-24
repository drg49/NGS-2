using UnityEngine;

public class BedInteract : Interactable
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private GameObject player;

    public override void Interact()
    {
        fadeAnimator.SetTrigger("FadeIntoBed");
        FirstPersonController controller = player.GetComponent<FirstPersonController>();
        controller.enabled = false;
        Destroy(gameObject);
    }
}
