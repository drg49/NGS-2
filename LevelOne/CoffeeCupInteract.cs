using UnityEngine;

public class CoffeeCupInteract : Interactable
{
    [SerializeField] private GameObject fpsCoffeePot;
    [SerializeField] private GameObject fpsCoffeePotPour;

    public override void Interact()
    {
        Destroy(fpsCoffeePot);
        // Disable coffee cup click
        BoxCollider box = GetComponent<BoxCollider>();
        box.enabled = false;
        fpsCoffeePotPour.SetActive(true);
    }

}
