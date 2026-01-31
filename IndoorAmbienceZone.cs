using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IndoorAmbienceZone : MonoBehaviour
{
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private AudioListener playerCameraListener;
    [SerializeField] private PlayerInteraction playerInteraction;

    private AmbienceController ambience;

    // Track how many colliders the player is currently inside
    private int playerInsideCount = 0;

    // Track whether the player has been disabled indoors
    private bool playerDisabledIndoors = false;

    public bool PlayerDisabledIndoors => playerDisabledIndoors; // public getter

    private void Awake()
    {
        ambience = FindFirstObjectByType<AmbienceController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInsideCount++;
        if (playerInsideCount == 1) // First collider entered
        {
            ambience?.EnterIndoor();

            FirstPersonController fpc = other.GetComponent<FirstPersonController>();
            if (fpc != null)
                fpc.SetFootsteps(true); // use secondary (indoor) footsteps
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInsideCount--;
        if (playerInsideCount <= 0) // Last collider exited
        {
            playerInsideCount = 0; // safety check
            ambience?.ExitIndoor();

            FirstPersonController fpc = other.GetComponent<FirstPersonController>();
            if (fpc != null)
                fpc.SetFootsteps(false); // revert to default footsteps
        }
    }

    public void DisablePlayerIndoors()
    {
        playerController.enabled = false;
        playerCameraListener.enabled = false;
        playerInteraction.enabled = false;

        playerDisabledIndoors = true; // track state
    }

    public void EnablePlayerIndoors()
    {
        playerController.enabled = true;
        playerCameraListener.enabled = true;
        playerInteraction.enabled = true;

        playerDisabledIndoors = false; // reset state
    }
}
