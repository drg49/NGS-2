using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorInteract : Interactable
{
    [Header("Objectives")]
    [SerializeField] private PauseMenuObjectivesController objectivesController;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private TextMeshProUGUI instructionalText;

    public override void Interact()
    {
        GoOutside();

        if (objectivesController != null)
        {
            objectivesController.SetObjectives(
                new List<string>
                {
                    "Make coffee", // completed
                    "Wash up", // completed
                    "Leave your apartment",
                    ""
                },
                3
            );
        }
    }

    private void GoOutside()
    {
        instructionalText.text = "";
        //fadeAnimator.SetTrigger("FadeInOut");
        Destroy(gameObject);
    }
}
