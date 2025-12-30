using UnityEngine;

public class CoffeeCupInteract : Interactable
{
    [SerializeField] private GameObject fpsCoffeePot;

    public override void Interact()
    {
        Destroy(fpsCoffeePot);
    }

}
