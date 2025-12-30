using UnityEngine;

public class CoffeeBagInteract : Interactable
{
    [SerializeField] private GameObject coffeePot;
    [SerializeField] private GameObject fpsCoffeeBag;
    [SerializeField] private AudioSource grab;

    private BoxCollider coffeePotCollider;

    public override void Interact()
    {
        fpsCoffeeBag.SetActive(true);
        grab.Play();
        ActivateCoffePot();
        Destroy(gameObject);
    }

    private void ActivateCoffePot()
    {
        coffeePotCollider = coffeePot.GetComponent<BoxCollider>();
        coffeePotCollider.enabled = true;
    }
}
