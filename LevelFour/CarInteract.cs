using UnityEngine;

public class CarInteract : Interactable
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject carPlayer;
    [SerializeField] private CarController carScript;
    [SerializeField] private GameObject marcus;
    [SerializeField] private GameObject david;
    [SerializeField] private GameObject marcusCarPlayer;
    [SerializeField] private GameObject davidCarPlayer;
    [SerializeField] private GameObject driveInstruction;

    public override void Interact()
    {
        // Switch to car player
        Destroy(player);
        carPlayer.SetActive(true);
        carScript.enabled = true;

        // Destroy standing players
        Destroy(marcus);
        Destroy(david);

        // Activate Marcus & David inside the car
        marcusCarPlayer.SetActive(true);
        davidCarPlayer.SetActive(true);

        driveInstruction.SetActive(true);

        Destroy(gameObject);
    }
}
