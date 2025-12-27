using UnityEngine;
using System.Collections.Generic;

public class ShowerInteract : Interactable
{
    [Header("Objectives")]
    [SerializeField] private PauseMenuObjectivesController objectivesController;
    [SerializeField] private Animator fadeAnimator; // assign via inspector

    public override void Interact()
    {
        TurnOnShower();

        if (objectivesController != null)
        {
            objectivesController.SetObjectives(
                new List<string>
                {
                    "Wash up", // completed objective (grayed out)
                    "" // next active objective
                },
                1
            );
        }
    }

    private void TurnOnShower()
    {
        fadeAnimator.SetTrigger("FadeInOut");
        Destroy(gameObject);
    }
}
