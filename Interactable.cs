using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [Header("Interaction Settings")]
    public string interactionText = ""; // Optional per object

    /// <summary>
    /// Called when the player interacts
    /// </summary>
    public virtual void Interact()
    {
        // Custom behavior will be implemented in child classes
    }
}
