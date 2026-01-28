using UnityEngine;

public class LevelFourDebugManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject carInteract;
    [SerializeField] GameObject carPlayer;
    [SerializeField] CarController carController;
    [SerializeField] GameObject marcusCarPlayer;
    [SerializeField] GameObject davidCarPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(player);
        Destroy(carInteract);
        carPlayer.SetActive(true);
        marcusCarPlayer.SetActive(true);
        davidCarPlayer.SetActive(true);
        carController.enabled = true;
    }
}
