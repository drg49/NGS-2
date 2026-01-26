using UnityEngine;

public class CarInteract : Interactable
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject carPlayer;

    public override void Interact()
    {
        Destroy(player);
        carPlayer.SetActive(true);
        Destroy(gameObject);
    }
}
