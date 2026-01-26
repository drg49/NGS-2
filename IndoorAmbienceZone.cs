using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IndoorAmbienceZone : MonoBehaviour
{
    private AmbienceController ambience;

    // Track how many colliders the player is currently inside
    private int playerInsideCount = 0;

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

            FirstPersonController playerController = other.GetComponent<FirstPersonController>();
            if (playerController != null)
                playerController.SetFootsteps(true); // use secondary (indoor) footsteps
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

            FirstPersonController playerController = other.GetComponent<FirstPersonController>();
            if (playerController != null)
                playerController.SetFootsteps(false); // revert to default footsteps
        }
    }
}
