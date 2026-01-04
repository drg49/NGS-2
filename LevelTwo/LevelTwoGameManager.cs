using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelTwoGameManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    [Header("References")]
    [SerializeField] private GameObject player;      // The player GameObject
    [SerializeField] private TextMeshProUGUI interactionText; // The interaction UI text

    [Header("Pause Reference")]
    [SerializeField] private PauseMenuController pauseMenu;

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
        if (pauseMenu != null && pauseMenu.IsPaused)
            return;

        // Clear interaction text
        if (interactionText != null)
            interactionText.text = "";
    }
}
