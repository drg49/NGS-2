using UnityEngine;

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
    //[SerializeField] private GameObject logObjective;

    void Start()
    {
        //Destroy(carColliders);
        //Destroy(car);
        //Destroy(enterCampsite);
        ////player.SetActive(true);
        //parkedCar.SetActive(true);
        //dialogueOneCam.SetActive(true);
        //dialogueOne.SetActive(true);
        fadeAnim.SetTrigger("FadeIntoCamp");
        //fadeAnim.SetTrigger("SetUpTent");
        //logObjective.SetActive(true);
    }
}
