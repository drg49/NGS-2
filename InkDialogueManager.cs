using System.Collections;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;

public class InkDialogueManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel; // DialoguePanel GameObject
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject optionOneGO;
    [SerializeField] private GameObject optionTwoGO;
    [SerializeField] private TextMeshProUGUI optionOneText;
    [SerializeField] private TextMeshProUGUI optionTwoText;

    [Header("Typewriter Settings")]
    [SerializeField] private float textSpeed = 0.05f;

    [Header("Input Actions")]
    [SerializeField] private PlayerInputActions inputActions; // Your PlayerInputActions asset

    private Story currentStory;
    private bool isTyping = false;

    private void Awake()
    {
        // Instantiate the PlayerInputActions class
        inputActions = new PlayerInputActions();

        // Enable the Dialogue map so we can subscribe to the Submit action
        inputActions.Dialogue.Enable();
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
        // Switch maps
        inputActions.Player.Disable();
        inputActions.Dialogue.Enable();

        dialoguePanel.SetActive(true);
        currentStory = new Story(inkJSON.text);
        ContinueStory();
    }

    // -------------------- Continue Story --------------------
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string nextLine = currentStory.Continue().Trim();
            StopAllCoroutines();
            StartCoroutine(TypeText(nextLine));
        }
        else if (currentStory.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else
        {
            EndStory();
        }
    }

    // -------------------- Typewriter Effect --------------------
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
    }

    // -------------------- Display Choices --------------------
    private void DisplayChoices()
    {
        int choiceCount = currentStory.currentChoices.Count;

        optionOneGO.SetActive(choiceCount >= 1);
        optionTwoGO.SetActive(choiceCount == 2);

        if (choiceCount >= 1)
            optionOneText.text = currentStory.currentChoices[0].text;

        if (choiceCount == 2)
            optionTwoText.text = currentStory.currentChoices[1].text;
    }

    // -------------------- Handle Submit --------------------
    private void OnSubmitOption(InputAction.CallbackContext context)
    {
        if (isTyping)
        {
            return;
        }

        int choiceIndex = -1;

        // Corrected checks
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
        optionOneGO.SetActive(false);
        optionTwoGO.SetActive(false);
        dialoguePanel.SetActive(false);

        // Switch back to Player map
        inputActions.Dialogue.Disable();
        inputActions.Player.Enable();
    }
}
