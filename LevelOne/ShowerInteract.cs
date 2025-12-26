using UnityEngine;
using System.Collections.Generic;

public class ShowerInteract : Interactable
{
    [Header("Objectives")]
    [SerializeField] private PauseMenuObjectivesController objectivesController;

    private void Awake()
    {
        interactionText = "Turn on the shower";
    }

    public override void Interact()
    {
        TurnOnShower();

        if (objectivesController != null)
        {
            objectivesController.SetObjectives(
                new List<string>
                {
                    "",                 // completed objective (grayed out)
                    "Take a shower"     // next active objective
                },
                0
            );
        }
    }

    private void TurnOnShower()
    {
        Debug.Log("Shower turned on");
        // animations / sounds later
    }
}
