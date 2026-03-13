using System.Collections.Generic;
using UnityEngine;

public class LevelSixDebugManager : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private GameObject preventCabinExploreCollider;
    [SerializeField] private GameObject foundCabinTrigger;
    [SerializeField] private GameObject notifyFriends;
    [SerializeField] private GameObject notifyDavid;
    [SerializeField] private PathWalker davidPW;
    [SerializeField] private PathWalker marcusPW;
    [SerializeField] private GameObject fpsRifle;
    [SerializeField] private GameObject huntingArea;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject eatDinnerTrigger;
    [SerializeField] private GameObject doorKnock;
    [SerializeField] private Animator fadeAnim;
    [SerializeField] private List<GameObject> davidWaypointsToDisable;

    [SerializeField] private float debugRunSpeed = 8f;

    // player run speed = 5.5
    // player, Marcus, David walk speed = 1.5

    void Start()
    {
        ApplyDebugSpeed(); 
        //notifyDavid.SetActive(false);
        notifyDavid.SetActive(true);
        //DisableGameObjects(davidWaypointsToDisable);
        //fadeAnim.SetTrigger("FadeInOutCarryMarcus");
        //fadeAnim.SetTrigger("FadeInOutCarryMarcusCabin");
        //fadeAnim.SetTrigger("FadeInOutDinner");
        preventCabinExploreCollider.SetActive(false);
        foundCabinTrigger.SetActive(false);
        fpsController.canRun = true;
        notifyFriends.SetActive(false);
        eatDinnerTrigger.SetActive(true);
        doorKnock.SetActive(false);
        //DebugRabbits();
    }

    private void DisableGameObjects(IEnumerable<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    void DebugRabbits()
    {
        player.SetActive(false);
        player.SetActive(true);
        fpsRifle.SetActive(true);
        huntingArea.SetActive(true);
    }

    private void ApplyDebugSpeed()
    {
        if (fpsController != null)
        {
            fpsController.runSpeed = debugRunSpeed;
            fpsController.canRun = true;
        }

        if (davidPW != null)
        {
            davidPW.SetMoveSpeed(debugRunSpeed);
        }

        if (marcusPW != null)
        {
            marcusPW.SetMoveSpeed(debugRunSpeed);
        }
    }
}
