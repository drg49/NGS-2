using UnityEngine;

public class FirepitInteract : Interactable
{
    [SerializeField] private GameObject logPile;
    [SerializeField] private GameObject talkToMarcus;
    [SerializeField] private GameObject logText;
    [SerializeField] private AudioSource dropWoodAudio;

    public override void Interact()
    {
        logPile.SetActive(true);
        dropWoodAudio.Play();
        talkToMarcus.SetActive(true);
        Destroy(logText);
        Destroy(gameObject);
    }
}
