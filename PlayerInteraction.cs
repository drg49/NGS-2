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

    private PlayerInputActions inputActions;

    private void Awake()
    {
        if (cam == null)
            cam = GetComponent<Camera>(); // Automatically use camera on same object if not assigned

        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack.performed += OnAttack; // Listen for Attack input
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Disable();
    }

    private void Update()
    {
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
                // Hovering over clickable object
                reticle.color = Color.red;

                if (interactionTextUI != null)
                {
                    interactionTextUI.text = interactable.interactionText;
                    interactionTextUI.gameObject.SetActive(true);
                }

                return; // Exit early since we found an interactable
            }
        }

        // Not hovering over interactable
        reticle.color = Color.white;
        if (interactionTextUI != null)
            interactionTextUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when Attack input is pressed
    /// </summary>
    private void OnAttack(InputAction.CallbackContext context)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(); // Calls log + UnityEvent
            }
        }
    }
}
