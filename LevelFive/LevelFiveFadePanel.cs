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
}
