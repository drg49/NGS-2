using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoffeeCupInteract : Interactable
{
    [SerializeField] private GameObject fpsCoffeePot;
    [SerializeField] private GameObject fpsCoffeePotPour;
    [SerializeField] private GameObject fpsCoffeeCup;
    [SerializeField] private GameObject coffeeCup;
    [SerializeField] private GameObject coffeeLiquid;
    [SerializeField] private AudioSource grab;
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private TextMeshProUGUI instructionalText;

    private BoxCollider box;
    private bool coffeeFinished = false;

    private void Awake()
    {
        inputActions ??= new PlayerInputActions();
        box = GetComponent<BoxCollider>();
    }

    public override void Interact()
    {
        if (!coffeeFinished)
        {
            StartPouring();
        }
        else
        {
            // Not the whole object (holding this script), just the coffee cup without the plate
            Destroy(coffeeCup);
            Destroy(coffeeLiquid);
            grab.Play();
            fpsCoffeeCup.SetActive(true);
            box.enabled = false;
            string button = inputActions.Player.Interact.bindings[0].ToDisplayString();
            instructionalText.text = $"Hold [{button}] to drink";

            // Force alpha to visible, will need to set it back later
            Color c = instructionalText.color;
            c.a = 1f; // 1 = 255
            instructionalText.color = c;
        }
    }

    private void StartPouring()
    {
        Destroy(fpsCoffeePot);
        // Disable interaction while pouring
        box.enabled = false;
        fpsCoffeePotPour.SetActive(true);
    }

    // Called from Animation Event at END of pour animation
    public void OnPourFinished()
    {
        coffeeFinished = true;

        // Re-enable interaction
        box.enabled = true;

        // Update interaction prompt
        interactionText = "Grab Coffee Cup";
    }
}
