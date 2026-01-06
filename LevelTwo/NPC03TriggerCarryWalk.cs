using UnityEngine;

public class NPC03TriggerCarryWalk : MonoBehaviour
{
    [SerializeField] private Animator npcThreeAnimator;
    [SerializeField] private GameObject trayOfFood;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("NPC")) return;
        npcThreeAnimator.SetTrigger("FemaleCarryWalk");
        trayOfFood.SetActive(true);
        Destroy(gameObject);
    }
}
