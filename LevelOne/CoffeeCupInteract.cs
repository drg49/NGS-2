using UnityEngine;

public class CoffeeCupInteract : Interactable
{
    [SerializeField] private GameObject fpsCoffeePot;

    public override void Interact()
    {
        Destroy(fpsCoffeePot);
        // Disable coffee cup click
        BoxCollider box = GetComponent<BoxCollider>();
        box.enabled = false;
    }

}
