using UnityEngine;

public class SetUpTent : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        fadeAnimator.SetTrigger("SetUpTent");
        Destroy(gameObject);
    }
}
