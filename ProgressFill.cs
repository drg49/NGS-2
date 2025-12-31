using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ProgressFill : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] protected PlayerInputActions inputActions;

    [Header("Progress Settings")]
    [SerializeField] protected float duration = 10f;

    [Header("UI")]
    [Tooltip("Root object of the progress bar (e.g. DrinkProgress)")]
    [SerializeField] protected GameObject progressBarRoot;

    [Tooltip("Fill image inside the progress bar")]
    [SerializeField] protected Image progressFill;

    protected float progress;
    protected bool isHolding;
    protected bool isComplete;

    protected virtual void Awake()
    {
        if (inputActions == null)
            inputActions = new PlayerInputActions();
    }

    protected virtual void OnEnable()
    {
        ResetProgress();

        // Show progress bar
        if (progressBarRoot != null)
            progressBarRoot.SetActive(true);

        // Enable input
        inputActions.Player.Enable();

        inputActions.Player.Interact.started += OnInteractStarted;
        inputActions.Player.Interact.canceled += OnInteractCanceled;
    }

    protected virtual void OnDisable()
    {
        inputActions.Player.Interact.started -= OnInteractStarted;
        inputActions.Player.Interact.canceled -= OnInteractCanceled;

        inputActions.Player.Disable();

        if (progressBarRoot != null)
            progressBarRoot.SetActive(false);
    }

    protected virtual void Update()
    {
        if (!isHolding || isComplete)
            return;

        progress += Time.deltaTime;

        if (progressFill != null)
            progressFill.fillAmount = progress / duration;

        if (progress >= duration)
        {
            Complete();
        }
    }

    protected virtual void OnInteractStarted(InputAction.CallbackContext ctx)
    {
        isHolding = true;
    }

    protected virtual void OnInteractCanceled(InputAction.CallbackContext ctx)
    {
        isHolding = false;
    }

    protected virtual void Complete()
    {
        isComplete = true;
        isHolding = false;

        if (progressFill != null)
            progressFill.fillAmount = 1f;

        if (progressBarRoot != null)
            progressBarRoot.SetActive(false);
    }

    protected virtual void ResetProgress()
    {
        progress = 0f;
        isHolding = false;
        isComplete = false;

        if (progressFill != null)
            progressFill.fillAmount = 0f;
    }
}
