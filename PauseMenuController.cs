using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject objectivesPanel;
    [SerializeField] private GameObject controlsPanel;

    [Header("Player Reference")]
    [SerializeField] private FirstPersonController playerController;

    [Header("Settings UI")]
    [SerializeField] private Slider lookSensitivitySlider;
    [SerializeField] private Toggle dialogueSoundToggle;

    private PlayerInputActions inputActions;

    public bool IsPaused { get; private set; }
    public bool IsDialogueSoundEnabled => dialogueSoundToggle.isOn;
    public float LookSensitivity => lookSensitivitySlider.value;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        // Load saved settings or defaults
        float savedLookSensitivity = PlayerPrefs.GetFloat("LookSensitivity", 0.1f);
        bool savedDialogueSound = PlayerPrefs.GetInt("DialogueSoundEnabled", 1) == 1;

        // Apply to UI
        if (lookSensitivitySlider != null)
            lookSensitivitySlider.value = savedLookSensitivity;

        if (dialogueSoundToggle != null)
            dialogueSoundToggle.isOn = savedDialogueSound;

        // Apply to player immediately
        if (playerController != null)
            playerController.lookSensitivity = savedLookSensitivity;
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Pause.performed += TogglePause;

        // Hook UI events
        if (lookSensitivitySlider != null)
            lookSensitivitySlider.onValueChanged.AddListener(OnLookSensitivityChanged);

        if (dialogueSoundToggle != null)
            dialogueSoundToggle.onValueChanged.AddListener(OnDialogueSoundChanged);
    }

    private void OnDisable()
    {
        inputActions.UI.Pause.performed -= TogglePause;
        inputActions.UI.Disable();

        // Unhook UI events
        if (lookSensitivitySlider != null)
            lookSensitivitySlider.onValueChanged.RemoveListener(OnLookSensitivityChanged);

        if (dialogueSoundToggle != null)
            dialogueSoundToggle.onValueChanged.RemoveListener(OnDialogueSoundChanged);
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        if (IsPaused) Resume();
        else Pause();
    }

    private void Pause()
    {
        IsPaused = true;
        pauseMenuUI.SetActive(true);
        ShowMainMenu();

        Time.timeScale = 0f;
        AudioListener.pause = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerController != null)
            playerController.SetPaused(true);
    }

    public void Resume()
    {
        IsPaused = false;
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerController != null)
            playerController.SetPaused(false);
    }

    private void ShowMainMenu()
    {
        menuPanel.SetActive(true);
        objectivesPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void ShowObjectives()
    {
        menuPanel.SetActive(false);
        objectivesPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void ShowControls()
    {
        menuPanel.SetActive(false);
        objectivesPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    // -------------------- Reset Controls --------------------
    public void ResetControlsToDefault()
    {
        // Default values
        float defaultLookSensitivity = 0.1f;
        bool defaultDialogueSound = true;

        // Apply to UI
        if (lookSensitivitySlider != null)
            lookSensitivitySlider.value = defaultLookSensitivity;

        if (dialogueSoundToggle != null)
            dialogueSoundToggle.isOn = defaultDialogueSound;

        // Apply to player
        if (playerController != null)
            playerController.lookSensitivity = defaultLookSensitivity;

        // Save to PlayerPrefs
        PlayerPrefs.SetFloat("LookSensitivity", defaultLookSensitivity);
        PlayerPrefs.SetInt("DialogueSoundEnabled", defaultDialogueSound ? 1 : 0);
        PlayerPrefs.Save();
    }


    public void BackToMenu() => ShowMainMenu();
    public void QuitGame() => Application.Quit();

    // -------------------- Settings Callbacks --------------------

    private void OnLookSensitivityChanged(float newValue)
    {
        if (playerController != null)
            playerController.lookSensitivity = newValue;

        PlayerPrefs.SetFloat("LookSensitivity", newValue);
        PlayerPrefs.Save();
    }

    private void OnDialogueSoundChanged(bool enabled)
    {
        PlayerPrefs.SetInt("DialogueSoundEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }
}
