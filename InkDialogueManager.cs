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

    private Story currentStory;
    private bool isTyping = false;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        optionOneGO.SetActive(false);
        optionTwoGO.SetActive(false);
    }

    public void StartStory(TextAsset inkJSON)
    {
        dialoguePanel.SetActive(true);
        currentStory = new Story(inkJSON.text);
        ContinueStory();
    }

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
    }

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

    public void OnSubmitOption(InputAction.CallbackContext context)
    {
        if (context.performed && !isTyping)
        {
            int choiceIndex = -1;

            if (Keyboard.current.digit1Key.wasPressedThisFrame || Gamepad.current.buttonSouth.wasPressedThisFrame)
                choiceIndex = 0;
            else if (Keyboard.current.digit2Key.wasPressedThisFrame || Gamepad.current.buttonEast.wasPressedThisFrame)
                choiceIndex = 1;

            if (choiceIndex >= 0 && choiceIndex < currentStory.currentChoices.Count)
            {
                currentStory.ChooseChoiceIndex(choiceIndex);
                optionOneGO.SetActive(false);
                optionTwoGO.SetActive(false);
                ContinueStory();
            }
        }
    }

    private void EndStory()
    {
        dialogueText.text = "";
        optionOneGO.SetActive(false);
        optionTwoGO.SetActive(false);
        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue finished");
    }
}
