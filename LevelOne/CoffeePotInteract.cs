using UnityEngine;

public class CoffeePotInteract : Interactable
{
    [SerializeField] private GameObject fpsCoffeeBag;
    [SerializeField] private GameObject fpsCoffeePot;
    [SerializeField] private GameObject coffeeCup;
    [SerializeField] private AudioSource boilingWater;
    [SerializeField] private ParticleSystem steam;

    private bool hasInteractedOnce = false;

    public override void Interact()
    {
        if (!hasInteractedOnce)
        {
            hasInteractedOnce = true;

            Destroy(fpsCoffeeBag);
            boilingWater.Play();
            steam.Play();

            BoxCollider box = GetComponent<BoxCollider>();
            box.enabled = false;

            StartCoroutine(HandleAfterBoiling(box));
        }
        else
        {
            fpsCoffeePot.SetActive(true);
            BoxCollider coffeeCupCollider = coffeeCup.GetComponent<BoxCollider>();
            coffeeCupCollider.enabled = true;
            Destroy(boilingWater);
            Destroy(steam);
            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator HandleAfterBoiling(BoxCollider box)
    {
        yield return new WaitForSeconds(boilingWater.clip.length);

        steam.Stop();

        box.enabled = true;

        interactionText = "Grab Coffee Pot";
    }
}
