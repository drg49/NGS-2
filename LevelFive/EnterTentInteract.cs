using UnityEngine;

public class EnterTentInteract : Interactable
{
    [SerializeField] private Animator fadeAnimator;

    public override void Interact()
    {
        fadeAnimator.SetTrigger("EnterTent");
        Destroy(gameObject);
    }
}
