using UnityEngine;

public class DestroyOnEnter : MonoBehaviour
{
    [SerializeField] private GameObject waypoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Destroy(other.gameObject);
            Destroy(waypoints);
        }
    }
}
