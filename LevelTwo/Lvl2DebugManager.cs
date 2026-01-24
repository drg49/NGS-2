using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl2DebugManager : MonoBehaviour
{
    [SerializeField] private GameObject sitDownInteract;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject toiletPlayer;
    [SerializeField] private GameObject toiletInteract;
    [SerializeField] private GameObject jumpscareTrigger;
    [SerializeField] private GameObject leaveRestaurantTrigger;
    [SerializeField] private Animator fadeAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //fadeAnimator.SetTrigger("FadeSitDown");
        //fadeAnimator.SetTrigger("FadeLeaveConvo");
        //fadeAnimator.SetTrigger("FadeLeaveToilet");
        //fadeAnimator.SetTrigger("FadeToLevelThree");
        ////player.SetActive(false);
        ////toiletPlayer.SetActive(true);
        ////toiletInteract.SetActive(true);
        ////jumpscareTrigger.SetActive(true);
        ////sitDownInteract.SetActive(true);
        //leaveRestaurantTrigger.SetActive(true);

        SceneContext.CurrentLevelMode = LevelMode.LevelThree;
        // Reuse level 1 scene for level 3
        SceneManager.LoadScene("FirstLevel_Apartment");
    }
}
