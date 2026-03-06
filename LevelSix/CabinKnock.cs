using UnityEngine;

public class CabinKnock : Interactable
{
    [SerializeField] private AudioSource doorKnock;
    [SerializeField] private GameObject anybodyHomeDialogue;

    public override void Interact()
    {
        doorKnock.Play();
        anybodyHomeDialogue.SetActive(true);
        // Destroy this script component after interacting
        Destroy(this);
    }
}
