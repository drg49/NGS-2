using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SitDownInteract : Interactable
{
    [Header("Objectives")]
    [SerializeField] private PauseMenuObjectivesController objectivesController;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private TextMeshProUGUI instructionalText;

    public override void Interact()
    {
        StartConversation();

        if (objectivesController != null)
        {
            objectivesController.SetObjectives(
                new List<string>
                {
                    "Meet Marcus & David at Big Burger", // completed
                    "Order food at the register", // completed
                    "Sit down with Marcus & David", // completed
                    ""
                },
                2
            );
        }
    }

    private void StartConversation()
    {
        instructionalText.text = ""; // Clear instructional text
        fadeAnimator.SetTrigger("FadeSitDown");
        Destroy(gameObject);
    }
}
