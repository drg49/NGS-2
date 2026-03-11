using UnityEngine;

public class EatDinnerTrigger : MonoBehaviour
{
    [SerializeField] private Animator fadeAnim;
    [SerializeField] private GameObject huntingArea;
    [SerializeField] private GameObject rabbitManager;
    [SerializeField] private GameObject rabbitText;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Destroy hunting area, rabbit manager, and then the trigger itself");
        fadeAnim.SetTrigger("FadeInOutDinner");
        Destroy(huntingArea);
        Destroy(rabbitManager);
        Destroy(rabbitText);
        Destroy(gameObject);
    }
}
