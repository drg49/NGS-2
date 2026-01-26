using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShoppingListManager : MonoBehaviour
{
    public static ShoppingListManager Instance { get; private set; }

    [System.Serializable]
    public class ItemUI
    {
        public ShoppingItemType type;
        public TextMeshProUGUI text;
    }

    [Header("UI")]
    [SerializeField] private ItemUI[] itemTexts;

    private readonly HashSet<ShoppingItemType> collectedItems = new HashSet<ShoppingItemType>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void CollectItem(ShoppingItemType type)
    {
        if (collectedItems.Contains(type))
            return;

        collectedItems.Add(type);

        SetUITextGreen(type);
        DestroyAllInteractablesOfType(type);

        if (collectedItems.Count == itemTexts.Length)
        {
            Debug.Log("All items collected!");
        }
    }

    private void SetUITextGreen(ShoppingItemType type)
    {
        foreach (var item in itemTexts)
        {
            if (item.type == type)
            {
                item.text.color = Color.green;
                return;
            }
        }
    }

    private void DestroyAllInteractablesOfType(ShoppingItemType type)
    {
        var allInteractables = FindObjectsOfType<ShoppingItemInteractable>();

        foreach (var interactable in allInteractables)
        {
            if (interactable.ItemType == type)
            {
                Destroy(interactable.gameObject);
            }
        }
    }
}
