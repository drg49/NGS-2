using UnityEngine;

public class StartFire : Interactable
{
    [SerializeField] private Animator fadeAnimator;

    public override void Interact()
    {
        fadeAnimator.SetTrigger("FadeToNight");
        Destroy(this);
    }
}
