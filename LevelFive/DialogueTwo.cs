using TMPro;
using UnityEngine;

public class DialogueTwo : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueTwoInkJSON;
    [SerializeField] private GameObject campfirePlayer;
    [SerializeField] private GameObject nightCutsceneCamTwo;
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private TextMeshProUGUI instructionalText;

    private void Awake()
    {
        inputActions ??= new PlayerInputActions();
    }

    void OnEnable()
    {
        dialogueManager.OnDialogueFinished += EndDialogue;
    }

    void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= EndDialogue;
    }

    void Start()
    {
        dialogueManager.StartStory(dialogueTwoInkJSON);
    }

    private void EndDialogue()
    {
        Destroy(nightCutsceneCamTwo);
        campfirePlayer.SetActive(true);
        string button = inputActions.Player.Interact.bindings[0].ToDisplayString();
        instructionalText.text = $"Hold [{button}] to roast";

        // Force alpha to visible, will need to set it back later
        Color c = instructionalText.color;
        c.a = 1f; // 1 = 255
        instructionalText.color = c;
        Destroy(gameObject);
    }
}
