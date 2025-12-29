using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOneManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    [Header("References")]
    [SerializeField] private GameObject player;      // The player GameObject
    [SerializeField] private GameObject bedPlayer;   // The bed/bedPlayer GameObject
    [SerializeField] private TextMeshProUGUI interactionText; // The interaction UI text

    [Header("Pause Reference")]
    [SerializeField] private PauseMenuController pauseMenu; // Assign in Inspector
    public RenderTexture renderTexture;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = activeRT;
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

        // Switch objects
        player.SetActive(true);
        bedPlayer.SetActive(false);

        // Clear interaction text
        if (interactionText != null)
            interactionText.text = "";
    }
}
