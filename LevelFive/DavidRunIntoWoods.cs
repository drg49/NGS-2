using UnityEngine;
using UnityEngine.UI;

public class DavidRunIntoWoods : MonoBehaviour
{
    [SerializeField] private GameObject david;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lookForDavidObjective;
    [SerializeField] private GameObject midnightCustceneCamThree;
    [SerializeField] private Image reticleImage;

    private Collider triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("NPC"))
            return;

        // Destroy the collider so it can't fire again
        if (triggerCollider != null)
            Destroy(triggerCollider);

        david.SetActive(false);
        // Make reticle visible again
        reticleImage.enabled = true;
        // Switch back to player
        Destroy(midnightCustceneCamThree);
        player.SetActive(true);
        lookForDavidObjective.SetActive(true);
    }
}
