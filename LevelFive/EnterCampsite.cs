using UnityEngine;

public class EnterCampsite : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private CarController carController;

    private void OnTriggerEnter(Collider other)
    {
        fadeAnimator.SetTrigger("FadeIntoCamp");
        carController.enabled = false;
        Destroy(gameObject);
    }
}
