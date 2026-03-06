using UnityEngine;

public class DoorOpenClose : Interactable
{
    [SerializeField] private Animator doorAnimator;

    // Track door state
    private bool isOpen = false;

    private void Start()
    {
        // Set the initial interaction text
        interactionText = "Open Door";
    }

    // Open the door
    public void OpenDoor()
    {
        doorAnimator.SetTrigger("OpenDoor");
        isOpen = true;
        interactionText = "Close Door"; // Update text when door is open
    }

    // Close the door
    public void CloseDoor()
    {
        doorAnimator.SetTrigger("CloseDoor");
        isOpen = false;
        interactionText = "Open Door"; // Update text when door is closed
    }

    // Toggle door state
    public void ToggleDoor()
    {
        if (isOpen)
            CloseDoor();
        else
            OpenDoor();
    }

    public override void Interact()
    {
        ToggleDoor();
    }
}