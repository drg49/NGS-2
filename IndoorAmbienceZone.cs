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

    public bool PlayerDisabledIndoors => playerDisabledIndoors;

    private void Awake()
    {
        ambience = FindFirstObjectByType<AmbienceController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInsideCount++;
        if (playerInsideCount == 1) // First collider entered
        {
            ambience?.EnterIndoor(playerCameraListener.transform);
            other.GetComponent<FirstPersonController>()?.SetFootsteps(true); // indoor footsteps
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInsideCount--;
        if (playerInsideCount <= 0) // Last collider exited
        {
            playerInsideCount = 0;
            ambience?.ExitIndoor(playerCameraListener.transform);
            other.GetComponent<FirstPersonController>()?.SetFootsteps(false); // revert footsteps
        }
    }

    public void DisablePlayerIndoors()
    {
        playerController.enabled = false;
        playerCameraListener.enabled = false;
        playerInteraction.enabled = false;
        playerDisabledIndoors = true;
    }

    public void EnablePlayerIndoors()
    {
        playerController.enabled = true;
        playerCameraListener.enabled = true;
        playerInteraction.enabled = true;
        playerDisabledIndoors = false;
    }
}
