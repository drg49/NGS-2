using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC03Dialogue : NPCDialogue
{
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject instructionTwo;
    [SerializeField] private TextMeshProUGUI instructionalText;

    private PlayerInputActions input;
    private bool canListenForHold;
    private bool hasTriggeredHold;

    private void Awake()
    {
        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        if (dialogueManager != null)
            dialogueManager.OnDialogueFinished += DialogueEnded;
    }

    private void OnDisable()
    {
        if (dialogueManager != null)
            dialogueManager.OnDialogueFinished -= DialogueEnded;

        DisableHoldListener();
    }

    private void OnDestroy()
    {
        DisableHoldListener();
    }

    public override void Interact()
    {
        base.Interact();

        gameObject.layer = LayerMask.NameToLayer("Default");
        Destroy(instructionTwo);
    }

    private void DialogueEnded()
    {
        // Reticle stays off
        reticle.SetActive(false);

        // NPC begins moving / animating
        PathWalker pathWalker = GetComponent<PathWalker>();
        pathWalker.enabled = true;
    }

    // =========================
    // ANIMATOR EVENT CALLS THIS
    // =========================
    public void EnablePostDialogueHold()
    {
        if (hasTriggeredHold)
            return;

        string button = input.Player.Interact.bindings[0].ToDisplayString();
        instructionalText.text = $"Hold [{button}] to take food";

        // Force alpha to visible, will need to set it back later
        Color c = instructionalText.color;
        c.a = 1f; // 1 = 255
        instructionalText.color = c;

        canListenForHold = true;

        input.Player.Interact.performed += OnInteractHeld;
        input.Player.Enable();
    }

    private void OnInteractHeld(InputAction.CallbackContext context)
    {
        if (!canListenForHold || hasTriggeredHold)
            return;

        hasTriggeredHold = true;
        canListenForHold = false;

        Debug.Log("Player held E after animator event.");
        instructionalText.text = "";

        DisableHoldListener();
    }

    private void DisableHoldListener()
    {
        input.Player.Interact.performed -= OnInteractHeld;

        if (input.Player.enabled)
            input.Player.Disable();
    }
}
