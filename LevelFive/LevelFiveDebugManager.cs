using UnityEngine;
using System.Collections;

public class LevelFiveDebugManager : MonoBehaviour
{
    [SerializeField] private GameObject carColliders;
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject parkedCar;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enterCampsite;
    [SerializeField] private GameObject dialogueOne;
    [SerializeField] private GameObject dialogueOneCam;
    [SerializeField] private Animator fadeAnim;
    [SerializeField] private GameObject marcus;
    [SerializeField] private GameObject[] objectsToDestroy;
    [SerializeField] private GameObject levelFiveFadePanel;

    private void Start()
    {
        //SkipCarScene();
        StartCoroutine(FastForward());
    }

    private void SkipCarScene()
    {
        fadeAnim.SetTrigger("FadeIntoCamp");
    }

    private IEnumerator FastForward()
    {
        // fade in
        fadeAnim.SetTrigger("FadeIntoCamp");

        // wait 5 seconds
        yield return new WaitForSeconds(5f);

        Destroy(dialogueOne);
        Destroy(dialogueOneCam);

        //// then next step
        fadeAnim.SetTrigger("SetUpTent");

        //// wait 5 seconds
        yield return new WaitForSeconds(5f);

        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }

        fadeAnim.SetTrigger("FadeToNight");

        yield return new WaitForSeconds(5f);

        fadeAnim.SetTrigger("LeaveCampfire");

        yield return new WaitForSeconds(5f);

        // Temporarily speed it up
        fadeAnim.speed = 10f; // 3x faster

        fadeAnim.SetTrigger("EnterTent");
    }
}
