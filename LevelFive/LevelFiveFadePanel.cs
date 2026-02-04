using UnityEngine;

public class LevelFiveFadePanel : MonoBehaviour
{
    // ===== Car =====
    [Header("Car")]
    [SerializeField] private GameObject carColliders;
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject parkedCar;

    // ===== Characters =====
    [Header("Characters")]
    [SerializeField] private GameObject marcus;
    [SerializeField] private Animator marcusAnim;
    [SerializeField] private GameObject david;
    [SerializeField] private Animator davidAnim;
    [SerializeField] private GameObject player;

    // ===== Dialogue =====
    [Header("Dialogue")]
    [SerializeField] private GameObject dialogueOneCam;
    [SerializeField] private GameObject dialogueOne;

    // ===== Targets =====
    [Header("Targets")]
    [SerializeField] private Transform playerTentTarget;
    [SerializeField] private Transform davidTentTarget;
    [SerializeField] private Transform marcusTentTarget;

    // ===== Objectives =====
    [Header("Objectives")]
    [SerializeField] private GameObject logObjective;

    // ===== Misc =====
    [Header("Misc")]
    [SerializeField] private GameObject marcusTent;
    [SerializeField] private GameObject davidTent;
    [SerializeField] private GameObject playerTent;
    [SerializeField] private GameObject marcusBag;
    [SerializeField] private GameObject davidBag;
    [SerializeField] private AudioSource ambience;

    public void EnterCampsite()
    {
        Destroy(carColliders);
        Destroy(car);
        ambience.Play();
        parkedCar.SetActive(true);
        marcus.SetActive(true);
        david.SetActive(true);
        // Turn On Camera
        dialogueOneCam.SetActive(true);
    }

    public void StartDialogueOne()
    {
        dialogueOne.SetActive(true);
    }

    public void SetUpTent()
    {
        marcusTent.SetActive(true);
        davidTent.SetActive(true);
        playerTent.SetActive(true);
        // Make sure player is not inside tent after activation
        player.transform.position = playerTentTarget.position;
        marcusAnim.SetTrigger("IdleAfterTentSetUp");
        davidAnim.SetTrigger("IdleAfterTentSetUp");
        // Position and rotate NPCs
        david.transform.SetPositionAndRotation(
            davidTentTarget.position,
            davidTentTarget.rotation
        );
        marcus.transform.SetPositionAndRotation(
            marcusTentTarget.position,
            marcusTentTarget.rotation
        );
        // Destroy the items in their hands
        Destroy(marcusBag);
        Destroy(davidBag);
    }

    public void ShowLogObjective()
    {
        logObjective.SetActive(true);
    }
}
