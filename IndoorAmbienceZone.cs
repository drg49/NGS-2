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

        ambience?.EnterIndoor();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        ambience?.ExitIndoor();
    }
}
