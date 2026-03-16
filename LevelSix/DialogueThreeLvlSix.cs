using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class DialogueThreeLvlSix : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueThreeInkJSON;
    [SerializeField] private GameObject tablePlayer;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerLeaveTableTarget;
    [SerializeField] private TextMeshProUGUI instructionalText;
    [SerializeField] private GameObject davidJumpscareTrigger;

    private PlayerInputActions inputActions;

    private bool canGetUp = false;
    private bool isHolding = false;

    private float holdTimer = 0f;
    private readonly float holdTimeRequired = 1.5f;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        dialogueManager.OnDialogueFinished += EndDialogue;

        inputActions.Player.Enable();
        inputActions.Player.Interact.started += OnInteractStarted;
        inputActions.Player.Interact.canceled += OnInteractCanceled;
    }

    void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= EndDialogue;

        inputActions.Player.Interact.started -= OnInteractStarted;
        inputActions.Player.Interact.canceled -= OnInteractCanceled;
        inputActions.Player.Disable();
    }

    void Start()
    {
        StartCoroutine(Wait());
    }

    void Update()
    {
        if (!canGetUp || !isHolding)
            return;

        holdTimer += Time.deltaTime;

        if (holdTimer >= holdTimeRequired)
        {
            LeaveTable();
        }
    }

    private void OnInteractStarted(InputAction.CallbackContext ctx)
    {
        if (!canGetUp)
            return;

        isHolding = true;
    }

    private void OnInteractCanceled(InputAction.CallbackContext ctx)
    {
        isHolding = false;
        holdTimer = 0f;
    }

    private void LeaveTable()
    {
        player.transform.SetPositionAndRotation(
            playerLeaveTableTarget.position,
            playerLeaveTableTarget.rotation
        );

        canGetUp = false;
        isHolding = false;

        Destroy(tablePlayer);
        player.SetActive(true);

        davidJumpscareTrigger.SetActive(true);

        Destroy(gameObject);
    }

    private void EndDialogue()
    {
        ShowGetUpPrompt();
    }

    private void ShowGetUpPrompt()
    {
        canGetUp = true;

        string button = inputActions.Player.Interact.bindings[0].ToDisplayString();

        instructionalText.text = $"Hold [{button}] to get up";

        // Make text visibile
        Color c = instructionalText.color;
        c.a = 1f;
        instructionalText.color = c;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(6f);
        dialogueManager.StartStory(dialogueThreeInkJSON);
    }
}