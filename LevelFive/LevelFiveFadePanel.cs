using UnityEngine;

public class LevelFiveFadePanel : MonoBehaviour
{
    [SerializeField] private GameObject carColliders;
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject parkedCar;
    [SerializeField] private GameObject marcus;
    [SerializeField] private GameObject david;
    [SerializeField] private GameObject dialogueOneCam;
    [SerializeField] private GameObject dialogueOne;
    [SerializeField] private GameObject marcusTent;
    [SerializeField] private GameObject davidTent;
    [SerializeField] private GameObject playerTent;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerTentTarget;
    [SerializeField] private GameObject logObjective;

    public void EnterCampsite()
    {
        Destroy(carColliders);
        Destroy(car);
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
    }

    public void ShowLogObjective()
    {
        logObjective.SetActive(true);
    }
}
