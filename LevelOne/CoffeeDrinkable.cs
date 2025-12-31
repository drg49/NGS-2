using TMPro;
using UnityEngine;

public class CoffeeDrinkable : ProgressFill
{
    [SerializeField] private TextMeshProUGUI instructionalText;

    protected override void Complete()
    {
        base.Complete();

        Debug.Log("Coffee consumed");

        instructionalText.text = "";

        // Apply effects here
        gameObject.SetActive(false);
    }
}
