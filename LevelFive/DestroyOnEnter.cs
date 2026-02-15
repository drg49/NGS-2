using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
{
    [SerializeField] private GameObject waypoints;
    [SerializeField] private GameObject dialogueThree;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Destroy(other.gameObject);
            dialogueThree.SetActive(true);
            Destroy(waypoints);
        }
    }
}
