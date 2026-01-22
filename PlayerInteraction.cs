using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Camera & Reticle")]
    [SerializeField] private Camera cam;
    [SerializeField] private Image reticle;
    [SerializeField] private TextMeshProUGUI interactionTextUI;

    [Header("Interaction Settings")]
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private LayerMask interactableLayer; // Interactable objects
    [SerializeField] private LayerMask blockingLayer;     // Walls or other blockers

    [Header("Pause Reference")]
    [SerializeField] private PauseMenuController pauseMenu;

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

    private void UpdateReticleAndInteractionText()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // Raycast against everything
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Check if the hit object is interactable
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

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (pauseMenu != null && pauseMenu.IsPaused)
            return;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // Raycast against everything
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
                interactable.Interact();
        }
    }


    private void ResetUI()
    {
        if (reticle != null)
            reticle.color = Color.white;

        if (interactionTextUI != null)
            interactionTextUI.gameObject.SetActive(false);
    }
}
