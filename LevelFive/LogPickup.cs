using UnityEngine;

public class LogPickup : Interactable
{
    [SerializeField] private LogManager manager;
    [SerializeField] private AudioSource grabAudio;

    public override void Interact()
    {
        manager.AddLog();
        grabAudio.Play();
        Destroy(gameObject);
    }
}
