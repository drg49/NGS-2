using UnityEngine;
using UnityEngine.UI;

public class DavidRunIntoWoods : MonoBehaviour
{
    [SerializeField] private GameObject david;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerNPC;
    [SerializeField] private GameObject lookForDavidObjective;
    [SerializeField] private GameObject midnightCustceneCamThree;
    [SerializeField] private Image reticleImage;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject flashlightLight;
    [SerializeField] private AudioSource flashlightAudio;
    [SerializeField] private GameObject forestJumpscare;
    [SerializeField] private GameObject parkedCar;

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
        playerNPC.SetActive(false);
        lookForDavidObjective.SetActive(true);
        flashlight.SetActive(true);
        flashlightLight.SetActive(true);
        flashlightAudio.Play();
        parkedCar.SetActive(true);
        forestJumpscare.SetActive(true);
    }
}
