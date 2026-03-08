using UnityEngine;

public class GunPickUp : Interactable
{
    [SerializeField] private AudioSource gunCock;
    [SerializeField] private GameObject fpsGun;

    public override void Interact()
    {
        Debug.Log("PickUpGun");
        gunCock.Play();
        fpsGun.SetActive(true);
        // Take gun off shelf, so deactivate it
        gameObject.SetActive(false);
    }
}
