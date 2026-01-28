using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsDisplayFormatted : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI controlsText;

    [Header("Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private InputActionReference runAction;   // ? ADDED
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private InputActionReference pauseAction;

    private void OnEnable()
    {
        UpdateControlsText();
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        UpdateControlsText();
    }

    public void UpdateControlsText()
    {
        if (controlsText == null) return;

        controlsText.text =
            $"Move: {moveAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions)}\n\n" +
            $"Look: {lookAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions)}\n\n" +
            $"Run: {runAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions)}\n\n" +
            $"Interact: {interactAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions)}\n\n" +
            $"Pause: {pauseAction.action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions)}";
    }
}
