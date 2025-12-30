using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShowerInteract : Interactable
{
    [Header("Objectives")]
    [SerializeField] private PauseMenuObjectivesController objectivesController;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private TextMeshProUGUI instructionalText;

    public override void Interact()
    {
        TurnOnShower();

        if (objectivesController != null)
        {
            objectivesController.SetObjectives(
                new List<string>
                {
                    "Make and drink coffee", // completed
                    "Wash up", // completed
                    ""
                },
                2
            );
        }
    }

    private void TurnOnShower()
    {
        instructionalText.text = ""; // Clear instructional text
        fadeAnimator.SetTrigger("FadeInOut");
        Destroy(gameObject);
    }
}
