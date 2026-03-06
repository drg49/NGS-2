using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorOpenClose : Interactable
{
    [SerializeField] private Animator doorAnimator;
    [Header("Audio")]
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;

    private AudioSource audioSource;
    private bool isOpen = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        interactionText = "Open Door";
    }

    public void OpenDoor()
    {
        doorAnimator.SetTrigger("OpenDoor");
        isOpen = true;
        interactionText = "Close Door";

        audioSource.Stop();
        audioSource.clip = openClip;
        audioSource.Play();
    }

    public void CloseDoor()
    {
        doorAnimator.SetTrigger("CloseDoor");
        isOpen = false;
        interactionText = "Open Door";

        audioSource.Stop();
        audioSource.clip = closeClip;
        audioSource.Play();
    }

    public void ToggleDoor()
    {
        if (isOpen) CloseDoor();
        else OpenDoor();
    }

    public override void Interact()
    {
        ToggleDoor();
    }
}