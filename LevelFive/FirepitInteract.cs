using UnityEngine;

public class FirepitInteract : Interactable
{
    [SerializeField] private GameObject logPile;
    [SerializeField] private GameObject talkToMarcus;
    [SerializeField] private AudioSource dropWoodAudio;

    public override void Interact()
    {
        logPile.SetActive(true);
        dropWoodAudio.Play();
        talkToMarcus.SetActive(true);
        Destroy(gameObject);
    }
}
