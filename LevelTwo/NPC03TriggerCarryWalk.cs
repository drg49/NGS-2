using UnityEngine;

public class NPC03TriggerCarryWalk : MonoBehaviour
{
    [SerializeField] private Animator npcThreeAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("NPC")) return;
        npcThreeAnimator.SetTrigger("FemaleCarryWalk");
        Destroy(gameObject);
    }
}
