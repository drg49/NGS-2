using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject objectivesPanel;
    [SerializeField] private GameObject controlsPanel;

    [Header("Player Reference")]
    [SerializeField] private FirstPersonController playerController;

    [Header("Settings")]
    [SerializeField] private bool dialogueSoundEnabled = true;
    [SerializeField] private float lookSensitivity = 1f;

    private PlayerInputActions inputActions;

    public bool IsPaused { get; private set; }
    public bool IsDialogueSoundEnabled => dialogueSoundEnabled;
    public float LookSensitivity => lookSensitivity;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        // Load saved settings or default
        dialogueSoundEnabled = PlayerPrefs.GetInt("DialogueSoundEnabled", 1) == 1;
        lookSensitivity = PlayerPrefs.GetFloat("LookSensitivity", 1f);

        // Apply to player immediately if available
        if (playerController != null)
            playerController.lookSensitivity = lookSensitivity;
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Pause.performed += TogglePause;
    }

    private void OnDisable()
    {
        inputActions.UI.Pause.performed -= TogglePause;
        inputActions.UI.Disable();
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

    public void BackToMenu()
    {
        ShowMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // -------------------- Dialogue Sound Setting --------------------

    public void SetDialogueSoundEnabled(bool enabled)
    {
        dialogueSoundEnabled = enabled;
        PlayerPrefs.SetInt("DialogueSoundEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    // -------------------- Look Sensitivity Setting --------------------

    public void SetLookSensitivity(float sensitivity)
    {
        lookSensitivity = sensitivity;

        if (playerController != null)
            playerController.lookSensitivity = lookSensitivity;

        PlayerPrefs.SetFloat("LookSensitivity", lookSensitivity);
        PlayerPrefs.Save();
    }
}
