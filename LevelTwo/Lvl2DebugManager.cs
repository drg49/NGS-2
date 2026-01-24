using UnityEngine;

public class Lvl2DebugManager : MonoBehaviour
{
    [SerializeField] private GameObject sitDownInteract;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject toiletPlayer;
    [SerializeField] private GameObject toiletInteract;
    [SerializeField] private GameObject jumpscareTrigger;
    [SerializeField] private Animator fadeAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fadeAnimator.SetTrigger("FadeSitDown");
        fadeAnimator.SetTrigger("FadeLeaveConvo");
        fadeAnimator.SetTrigger("FadeLeaveToilet");
        fadeAnimator.SetTrigger("FadeToLevelThree");
        //player.SetActive(false);
        //toiletPlayer.SetActive(true);
        //toiletInteract.SetActive(true);
        jumpscareTrigger.SetActive(true);
        //sitDownInteract.SetActive(true);
    }
}
