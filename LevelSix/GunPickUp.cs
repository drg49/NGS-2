using UnityEngine;

public class GunPickUp : Interactable
{
    [SerializeField] private AudioSource gunCock;

    public override void Interact()
    {
        Debug.Log("PickUpGun");
        gunCock.Play();
        // Take gun off shelf, so deactivate it
        gameObject.SetActive(false);
    }
}
