using System.Collections;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InkDialogueManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject optionOneGO;
    [SerializeField] private GameObject optionTwoGO;
    [SerializeField] private TextMeshProUGUI optionOneText;
    [SerializeField] private TextMeshProUGUI optionTwoText;

    [Header("Typewriter Settings")]
    [SerializeField] private float textSpeed = 0.05f;

    [Header("Pause")]
    [SerializeField] private PauseMenuController pauseMenu;

    [Header("Selection Feedback")]
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private Color normalButtonColor = Color.white;
    [SerializeField] private Color selectedButtonColor = new(1f, 0.6f, 0.2f);
    [SerializeField] private float selectionFlashDuration = 0.15f;

    public System.Action OnDialogueFinished;

    private PlayerInputActions inputActions;
    private Story currentStory;

    private bool isTyping;
    private bool awaitingFinalContinue;
    private bool isSelecting;

    private Image optionOneImage;
    private Image optionTwoImage;

    // -------------------- Unity Lifecycle --------------------

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Dialogue.Submit.performed += OnSubmitOption;

        optionOneImage = optionOneGO.GetComponent<Image>();
        optionTwoImage = optionTwoGO.GetComponent<Image>();
    }

    private void OnDestroy()
    {
        inputActions.Dialogue.Submit.performed -= OnSubmitOption;
    }

    private void Start()
    {
        optionOneGO.SetActive(false);
        optionTwoGO.SetActive(false);
    }

    // -------------------- Start Dialogue --------------------

    public void StartStory(TextAsset inkJSON)
    {
        awaitingFinalContinue = false;
        isSelecting = false;

        inputActions.Player.Disable();
        inputActions.Dialogue.Enable();

        dialoguePanel.SetActive(true);
        currentStory = new Story(inkJSON.text);

        ContinueStory();
    }

    // -------------------- Story Flow --------------------

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            StopAllCoroutines();
            StartCoroutine(TypeText(currentStory.Continue().Trim()));
        }
        else if (currentStory.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else
        {
            ShowFinalContinue();
        }
    }

    // -------------------- Typewriter --------------------

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;

        if (currentStory.currentChoices.Count > 0)
            DisplayChoices();
        else if (!currentStory.canContinue)
            ShowFinalContinue();
    }

    // -------------------- Choices --------------------

    private void DisplayChoices()
    {
        ResetButtonVisuals();

        int count = currentStory.currentChoices.Count;

        optionOneGO.SetActive(count >= 1);
        optionTwoGO.SetActive(count == 2);

        if (count >= 1)
            optionOneText.text = $"[{GetOptionDisplayKey(0)}] {currentStory.currentChoices[0].text}";

        if (count == 2)
            optionTwoText.text = $"[{GetOptionDisplayKey(1)}] {currentStory.currentChoices[1].text}";
    }

    private void ResetButtonVisuals()
    {
        if (optionOneImage) optionOneImage.color = normalButtonColor;
        if (optionTwoImage) optionTwoImage.color = normalButtonColor;
    }

    // -------------------- Cross-Platform Button/Key Labels --------------------

    private string GetOptionDisplayKey(int index)
    {
        // Keyboard
        if (Keyboard.current != null)
        {
            if (index == 0) return "1";
            if (index == 1) return "2";
        }

        // Gamepad
        if (Gamepad.current != null)
        {
            string layout = Gamepad.current.layout;

            if (layout.Contains("DualShock") || layout.Contains("DualSense")) // PlayStation
            {
                if (index == 0) return "×"; // Cross
                if (index == 1) return "?"; // Circle
            }
            else if (layout.Contains("Switch")) // Nintendo Switch
            {
                if (index == 0) return "B";
                if (index == 1) return "A";
            }
            else // Xbox / default
            {
                if (index == 0) return "A";
                if (index == 1) return "B";
            }
        }

        return "?";
    }

    // -------------------- Final Continue --------------------

    private void ShowFinalContinue()
    {
        awaitingFinalContinue = true;
        ResetButtonVisuals();

        optionOneGO.SetActive(true);
        optionTwoGO.SetActive(false);
        optionOneText.text = $"[{GetOptionDisplayKey(0)}] Continue";
    }

    // -------------------- Input --------------------

    private void OnSubmitOption(InputAction.CallbackContext context)
    {
        if (pauseMenu.IsPaused || isTyping || isSelecting)
            return;

        if (awaitingFinalContinue)
        {
            StartCoroutine(PlaySelectionFeedback(optionOneImage, EndStory));
            return;
        }

        int choiceIndex = -1;
        Image targetImage = null;

        if (context.control.name == "1" || context.control.name == "buttonSouth")
        {
            choiceIndex = 0;
            targetImage = optionOneImage;
        }
        else if (context.control.name == "2" || context.control.name == "buttonEast")
        {
            choiceIndex = 1;
            targetImage = optionTwoImage;
        }

        if (choiceIndex >= 0 && choiceIndex < currentStory.currentChoices.Count)
        {
            StartCoroutine(PlaySelectionFeedback(targetImage, () =>
            {
                currentStory.ChooseChoiceIndex(choiceIndex);
                optionOneGO.SetActive(false);
                optionTwoGO.SetActive(false);
                ContinueStory();
            }));
        }
    }

    // -------------------- Selection Feedback --------------------

    private IEnumerator PlaySelectionFeedback(Image target, System.Action onComplete)
    {
        isSelecting = true;

        uiAudioSource.Play();

        if (target)
            target.color = selectedButtonColor;

        yield return new WaitForSeconds(selectionFlashDuration);

        ResetButtonVisuals();
        isSelecting = false;

        onComplete?.Invoke();
    }

    // -------------------- End Dialogue --------------------

    private void EndStory()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);

        optionOneGO.SetActive(false);
        optionTwoGO.SetActive(false);

        inputActions.Dialogue.Disable();
        inputActions.Player.Enable();

        OnDialogueFinished?.Invoke();
    }
}
