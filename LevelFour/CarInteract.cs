using UnityEngine;

public class CarInteract : Interactable
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject carPlayer;
    [SerializeField] private CarController carScript;

    public override void Interact()
    {
        Destroy(player);
        carPlayer.SetActive(true);
        carScript.enabled = true;
        Destroy(gameObject);
    }
}
