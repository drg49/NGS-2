using System.Collections;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;

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
    [SerializeField] private PauseMenuController pauseMenu; // Assign in Inspector

    public System.Action OnDialogueFinished;

    private PlayerInputActions inputActions;
    private Story currentStory;

    private bool isTyping;
    private bool awaitingFinalContinue;

    // -------------------- Unity Lifecycle --------------------

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Dialogue.Submit.performed += OnSubmitOption;
    }

    private void OnDestroy()
    {
        inputActions.Dialogue.Submit.performed -= OnSubmitOption;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
        optionOneGO.SetActive(false);
        optionTwoGO.SetActive(false);
    }

    // -------------------- Start Dialogue --------------------

    public void StartStory(TextAsset inkJSON)
    {
        awaitingFinalContinue = false;

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
        {
            DisplayChoices();
        }
        else if (!currentStory.canContinue)
        {
            ShowFinalContinue();
        }
    }

    // -------------------- Choices --------------------

    private void DisplayChoices()
    {
        int count = currentStory.currentChoices.Count;

        optionOneGO.SetActive(count >= 1);
        optionTwoGO.SetActive(count == 2);

        if (count >= 1)
            optionOneText.text = currentStory.currentChoices[0].text;

        if (count == 2)
            optionTwoText.text = currentStory.currentChoices[1].text;
    }

    // -------------------- Final Continue --------------------

    private void ShowFinalContinue()
    {
        awaitingFinalContinue = true;

        optionOneGO.SetActive(true);
        optionTwoGO.SetActive(false);
        optionOneText.text = "Continue";
    }

    // -------------------- Input --------------------

    private void OnSubmitOption(InputAction.CallbackContext context)
    {
        if (pauseMenu.IsPaused)
            return;

        if (isTyping)
            return;

        if (awaitingFinalContinue)
        {
            EndStory();
            return;
        }

        int choiceIndex = -1;

        if (context.control.name == "1" || context.control.name == "buttonSouth")
            choiceIndex = 0;
        else if (context.control.name == "2" || context.control.name == "buttonEast")
            choiceIndex = 1;

        if (choiceIndex >= 0 && choiceIndex < currentStory.currentChoices.Count)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            optionOneGO.SetActive(false);
            optionTwoGO.SetActive(false);
            ContinueStory();
        }
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
