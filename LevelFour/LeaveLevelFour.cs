using UnityEngine;

public class LeaveLevelFour : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name}");
    }
}
