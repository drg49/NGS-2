using UnityEngine;
using System.Collections;

public class DavidStareAtWoods : MonoBehaviour
{
    [SerializeField] private Animator davidAnim;
    [SerializeField] private GameObject midnightCustceneCamTwo;
    [SerializeField] private GameObject midnightCustceneCamThree;
    [SerializeField] private GameObject dialogueSix;

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

        // Start the sequence
        StartCoroutine(TriggerSequence());
    }

    private IEnumerator TriggerSequence()
    {
        // Trigger animation immediately
        davidAnim.SetTrigger("StareAtWoods");

        // Destroy second camera and activate third camera
        Destroy(midnightCustceneCamTwo);
        midnightCustceneCamThree.SetActive(true);

        // Wait 2 seconds before activating dialogue
        yield return new WaitForSeconds(2f);
        dialogueSix.SetActive(true);
    }
}
