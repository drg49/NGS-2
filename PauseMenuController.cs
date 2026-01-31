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

    [Header("Indoor Ambience (Optional)")]
    [SerializeField] private IndoorAmbienceZone indoorZone; // can be null

    [Header("Settings UI")]
    [SerializeField] private Slider lookSensitivitySlider;

    private PlayerInputActions inputActions;

    public bool IsPaused { get; private set; }
    public float LookSensitivity => lookSensitivitySlider.value;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        // Load saved settings or defaults
        float savedLookSensitivity = PlayerPrefs.GetFloat("LookSensitivity", 0.1f);

        // Apply to UI
        if (lookSensitivitySlider != null)
            lookSensitivitySlider.value = savedLookSensitivity;

        // Apply to player immediately
        if (playerController != null)
            playerController.lookSensitivity = savedLookSensitivity;
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Pause.performed += TogglePause;

        if (lookSensitivitySlider != null)
            lookSensitivitySlider.onValueChanged.AddListener(OnLookSensitivityChanged);
    }

    private void OnDisable()
    {
        inputActions.UI.Pause.performed -= TogglePause;
        inputActions.UI.Disable();

        if (lookSensitivitySlider != null)
            lookSensitivitySlider.onValueChanged.RemoveListener(OnLookSensitivityChanged);
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
        {
            // Only re-enable player if indoor zone hasn't disabled them
            if (indoorZone == null || !indoorZone.PlayerDisabledIndoors)
            {
                playerController.SetPaused(false);
            }
        }
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
        float defaultLookSensitivity = 0.1f;

        if (lookSensitivitySlider != null)
            lookSensitivitySlider.value = defaultLookSensitivity;

        if (playerController != null)
            playerController.lookSensitivity = defaultLookSensitivity;

        PlayerPrefs.SetFloat("LookSensitivity", defaultLookSensitivity);
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
}
