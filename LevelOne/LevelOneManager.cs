using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOneManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    [Header("References")]
    [SerializeField] private GameObject fadePanel;   // The fadeout panel
    [SerializeField] private GameObject player;      // The player GameObject
    [SerializeField] private GameObject bedPlayer;   // The bed/bedPlayer GameObject
    [SerializeField] private TextMeshProUGUI interactionText; // The interaction UI text

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        // Switch objects as requested
        fadePanel.SetActive(false);
        player.SetActive(true);
        bedPlayer.SetActive(false);
        // Clear interaction text
        interactionText.text = "";
    }
}
