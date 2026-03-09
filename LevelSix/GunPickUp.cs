using UnityEngine;

public class GunPickUp : Interactable
{
    [SerializeField] private AudioSource gunCock;
    [SerializeField] private GameObject fpsGun;
    [SerializeField] private GameObject rabbitUIText;
    [SerializeField] private GameObject rabbitManager;

    public override void Interact()
    {
        gunCock.Play();
        fpsGun.SetActive(true);
        rabbitUIText.SetActive(true);
        rabbitManager.SetActive(true);
        // Take gun off shelf, so deactivate it
        gameObject.SetActive(false);
    }
}
