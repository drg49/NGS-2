using UnityEngine;

public class LevelFourDebugManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject carInteract;
    [SerializeField] GameObject carPlayer;
    [SerializeField] CarController carController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(player);
        Destroy(carInteract);
        carPlayer.SetActive(true);
        carController.enabled = true;
    }
}
