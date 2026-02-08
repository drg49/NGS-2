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

    private void Start()
    {

        //Destroy(dialogueOne);
        //Destroy(dialogueOneCam);
        // fade in
        fadeAnim.SetTrigger("FadeIntoCamp");

        // wait 5 seconds
        //yield return new WaitForSeconds(5f);

        //// then next step
        //fadeAnim.SetTrigger("SetUpTent");

        //// wait 5 seconds
        //yield return new WaitForSeconds(5f);

        //fadeAnim.SetTrigger("FadeToNight");
    }

    //private IEnumerator Start()
    //{

    //    //Destroy(dialogueOne);
    //    //Destroy(dialogueOneCam);
    //    // fade in
    //    fadeAnim.SetTrigger("FadeIntoCamp");

    //    // wait 5 seconds
    //    yield return new WaitForSeconds(5f);

    //    Destroy(dialogueOne);
    //    Destroy(dialogueOneCam);

    //    //// then next step
    //    fadeAnim.SetTrigger("SetUpTent");

    //    //// wait 5 seconds
    //    yield return new WaitForSeconds(5f);

    //    fadeAnim.SetTrigger("FadeToNight");
    //}
}
