using UnityEngine;
using System.Collections;

public class EnterCabin : MonoBehaviour
{
    [SerializeField] private GameObject doorKnockCollider;

    private void OnEnable()
    {
        StartCoroutine(DestroyAfterDelay(8f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Show instruction to enter the cabin
        InstructionSequence sequence = GetComponent<InstructionSequence>();
        sequence.Play();

        // Allow player to enter cabin
        Destroy(doorKnockCollider);
    }
}