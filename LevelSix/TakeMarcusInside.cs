using UnityEngine;

// Cutscene where David & Player NPC take Marcus inside
public class TakeMarcusInside : MonoBehaviour
{
    [SerializeField] private Animator fadeAnim;

    private Collider triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("NPC"))
            return;

        // Destroy the collider so it can't fire again
        if (triggerCollider != null)
            Destroy(triggerCollider);

        fadeAnim.SetTrigger("FadeInOutCarryMarcusCabin");
    }
}
