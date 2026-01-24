using UnityEngine;

public class LeaveRestaurant : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;

    private void OnTriggerEnter(Collider other)
    {
        // Optional: only trigger on player
        if (!other.CompareTag("Player"))
            return;

        fadeAnimator.SetTrigger("FadeToLevelThree");
        Destroy(gameObject);
    }
}
