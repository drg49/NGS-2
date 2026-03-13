using UnityEngine;

public class ConsumerInteract : Interactable
{
    [SerializeField] private Consumer consumer;

    public override void Interact()
    {
        consumer.enabled = true;
    }
}
