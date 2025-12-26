using TMPro;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI interactionText;

    [Header("Input")]
    [SerializeField] private PlayerInputActions inputActions; // Reference your generated PlayerInputActions

    private void Awake()
    {
        // Optional: initialize if not already
        inputActions ??= new PlayerInputActions();
    }

    // Called by Animation Event
    public void DisplayInstructionalText()
    {
        // Get the E key binding from Player/Interact
        string button = inputActions.Player.Interact.bindings[0].ToDisplayString();
        interactionText.text = $"Hold [{button}] to get out of bed";
    }
}
