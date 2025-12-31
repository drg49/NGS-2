using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class ProgressFill : MonoBehaviour
{
    public event Action OnFillStarted;
    public event Action OnFillCanceled;
    public event Action OnFillCompleted;

    [Header("Input")]
    [SerializeField] protected PlayerInputActions inputActions;

    [Header("Progress Settings")]
    [SerializeField] protected float duration = 10f;

    [Header("UI")]
    [SerializeField] protected GameObject progressBarRoot;
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

        if (progressBarRoot != null)
            progressBarRoot.SetActive(true);

        inputActions.Player.Enable();

        inputActions.Player.Interact.started += HandleInteractStarted;
        inputActions.Player.Interact.canceled += HandleInteractCanceled;
    }

    protected virtual void OnDisable()
    {
        inputActions.Player.Interact.started -= HandleInteractStarted;
        inputActions.Player.Interact.canceled -= HandleInteractCanceled;

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

    private void HandleInteractStarted(InputAction.CallbackContext ctx)
    {
        if (isComplete)
            return;

        isHolding = true;
        OnFillStarted?.Invoke();
    }

    private void HandleInteractCanceled(InputAction.CallbackContext ctx)
    {
        if (!isHolding)
            return;

        isHolding = false;
        OnFillCanceled?.Invoke();
    }

    protected virtual void Complete()
    {
        isComplete = true;
        isHolding = false;

        if (progressFill != null)
            progressFill.fillAmount = 1f;

        if (progressBarRoot != null)
            progressBarRoot.SetActive(false);

        OnFillCompleted?.Invoke();
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
