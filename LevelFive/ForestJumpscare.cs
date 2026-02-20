using UnityEngine;

public class ForestJumpscare : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Call Jumpscare in Forest");

        // Get all BoxColliders on this GameObject
        BoxCollider[] colliders = GetComponents<BoxCollider>();

        // Destroy them all
        foreach (BoxCollider bc in colliders)
        {
            Destroy(bc);
        }
    }
}