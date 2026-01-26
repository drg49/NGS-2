using UnityEngine;

public enum ShoppingItemType
{
    Water,
    Wine,
    Chocolate,
    Wafers,
    Chips,
    Cookies,
    Oil
}


public class ShoppingItemInteractable : Interactable
{
    [SerializeField] private ShoppingItemType itemType;
    [SerializeField] private AudioSource grabAudio;

    public ShoppingItemType ItemType => itemType;

    public override void Interact()
    {
        ShoppingListManager.Instance.CollectItem(itemType);
        grabAudio.Play();
    }
}