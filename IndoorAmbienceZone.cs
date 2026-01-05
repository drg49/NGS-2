using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IndoorAmbienceZone : MonoBehaviour
{
    private AmbienceController ambience;

    private void Awake()
    {
        ambience = FindFirstObjectByType<AmbienceController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Switch ambience
        ambience?.EnterIndoor();

        // Switch player footsteps
        FirstPersonController playerController = other.GetComponent<FirstPersonController>();
        if (playerController != null)
            playerController.SetFootsteps(true); // use secondary (indoor) footsteps
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Switch ambience back
        ambience?.ExitIndoor();

        // Switch player footsteps back
        FirstPersonController playerController = other.GetComponent<FirstPersonController>();
        if (playerController != null)
            playerController.SetFootsteps(false); // revert to default (outdoor) footsteps
    }
}
