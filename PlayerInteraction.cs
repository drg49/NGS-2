using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Camera & Reticle")]
    [SerializeField] private Camera cam;             // Player camera
    [SerializeField] private Image reticle;          // UI reticle
    [SerializeField] private TextMeshProUGUI interactionTextUI; // UI text prompt

    [Header("Interaction Settings")]
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private LayerMask interactableLayer; // Assign Interactable layer

    [Header("Pause Reference")]
    [SerializeField] private PauseMenuController pauseMenu; // Assign in Inspector

    private PlayerInputActions inputActions;

    private void Awake()
    {
        if (cam == null)
            cam = GetComponent<Camera>();

        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Disable();

        ResetUI();
    }

    private void Update()
    {
        if (pauseMenu != null && pauseMenu.IsPaused)
        {
            ResetUI();
            return;
        }

        UpdateReticleAndInteractionText();
    }

    /// <summary>
    /// Updates reticle color and shows interaction text if hovering over an interactable
    /// </summary>
    private void UpdateReticleAndInteractionText()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                reticle.color = Color.red;

                if (interactionTextUI != null)
                {
                    interactionTextUI.text = interactable.interactionText;
                    interactionTextUI.gameObject.SetActive(true);
                }
                return;
            }
        }

        ResetUI();
    }

    /// <summary>
    /// Called when Attack input is pressed
    /// </summary>
    private void OnAttack(InputAction.CallbackContext context)
    {
        if (pauseMenu != null && pauseMenu.IsPaused)
            return;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
                interactable.Interact();
        }
    }

    /// <summary>
    /// Resets reticle and interaction UI
    /// </summary>
    private void ResetUI()
    {
        if (reticle != null)
            reticle.color = Color.white;

        if (interactionTextUI != null)
            interactionTextUI.gameObject.SetActive(false);
    }
}
