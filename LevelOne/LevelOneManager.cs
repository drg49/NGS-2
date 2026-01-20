using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOneManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bedPlayer;
    [SerializeField] private TextMeshProUGUI interactionText;

    [Header("Pause Reference")]
    [SerializeField] private PauseMenuController pauseMenu;
    public RenderTexture renderTexture;

    // one-time interaction guard to determine if player has already left the bed
    private bool hasInteracted = false;

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
        // Player has already left the bed
        if (hasInteracted)
            return;

        // paused
        if (pauseMenu != null && pauseMenu.IsPaused)
            return;

        // mark as consumed
        hasInteracted = true;

        // CRITICAL: destroy first (active camera)
        Destroy(bedPlayer);
        player.SetActive(true);

        if (interactionText != null)
            interactionText.text = "";

        // Optional hard-disable input entirely after first use
        // inputActions.Player.Interact.Disable();
    }
}
