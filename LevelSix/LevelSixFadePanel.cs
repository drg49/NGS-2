using UnityEngine;
using System.Collections.Generic;

public class LevelSixFadePanel : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private GameObject notifyDavidCam;
    [SerializeField] private GameObject carryMarcusCam;
    [SerializeField] private GameObject marcusInBedCam;
    [SerializeField] private GameObject dialogueOneCam;

    [Header("Characters")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject marcus;
    [SerializeField] private Animator marcusAnim;
    [SerializeField] private PathWalker marcusPW;
    [SerializeField] private GameObject david;
    [SerializeField] private Animator davidAnim;
    [SerializeField] private PathWalker davidPW;
    [SerializeField] private GameObject playerNPC;
    [SerializeField] private Animator playerNPCAnim;
    [SerializeField] private PathWalker playerNPCPW;
    [SerializeField] private GameObject tablePlayer;

    [Header("Targets")]
    [SerializeField] private Transform playerNPCCarryMarcusTarget;
    [SerializeField] private Transform davidCarryMarcusTarget;
    [SerializeField] private Transform marcusBeingCarriedTarget;
    // Waypoints to disable during carry path
    [SerializeField] private List<GameObject> carryPathWaypointsToDisable;
    // Enter cabin targets
    [SerializeField] private GameObject marcusWP3;
    [SerializeField] private GameObject davidWP4;
    [SerializeField] private Transform playerNPCInCabinTarget;
    [SerializeField] private Transform davidInCabinTarget;
    [SerializeField] private Transform marcusInBedTarget;
    // Waypoints to disable when marcus is in bed
    [SerializeField] private List<GameObject> marcusInBedWaypointsToDisable;
    [SerializeField] private Transform davidCookDinnerTarget;

    [Header("Misc")]
    [SerializeField] private GameObject dialogueOne;
    [SerializeField] private GameObject fpsGun;
    [SerializeField] private GameObject kitchenObjects;


    // David and Player NPC carry Marcus into cabin
    public void CarryMarcusIntoCabin()
    {
        Destroy(notifyDavidCam);
        carryMarcusCam.SetActive(true);
        playerNPC.SetActive(true);

        // Position and rotate Player NPC
        playerNPC.transform.SetPositionAndRotation(
            playerNPCCarryMarcusTarget.position,
            playerNPCCarryMarcusTarget.rotation
        );
        // Position and rotate David
        david.transform.SetPositionAndRotation(
            davidCarryMarcusTarget.position,
            davidCarryMarcusTarget.rotation
        );
        // Position and rotate Marcus
        marcus.transform.SetPositionAndRotation(
            marcusBeingCarriedTarget.position,
            marcusBeingCarriedTarget.rotation
        );

        // Trigger NPC Animations
        davidAnim.SetTrigger("CarryMarcus");
        playerNPCAnim.SetTrigger("CarryMarcus");
        marcusAnim.SetTrigger("BeingCarried");
    }
    
    // Players start moving towards cabin
    public void ActivateCarryWaypoints()
    {
        // First path target for Player NPC, so just enable it's script
        playerNPCPW.enabled = true;

        // Disable waypoints previous to this 'Carry Walk' path
        DisableGameObjects(carryPathWaypointsToDisable);

        // Update NPC path speed and rotation
        marcusPW.SetMoveSpeed(0.8f);
        marcusPW.SetRotateSpeed(0);
        davidPW.SetMoveSpeed(0.8f);

        // Enable cabin targets for Marcus & David, then refresh their pathwalkers
        marcusWP3.SetActive(true);
        davidWP4.SetActive(true);
        RefreshMarcusPathwalker();
        RefreshDavidPathWalker();
    }

    // Marcus is laying in bed inside the cabin
    public void MarcusInBed()
    {
        Destroy(carryMarcusCam);
        marcusInBedCam.SetActive(true);

        // Disable waypoints previous to this 'Marcus in Bed' event
        DisableGameObjects(marcusInBedWaypointsToDisable);

        // Don't need pathwalkers right now
        // Can reactivate later (causing a pathwalker refresh)
        davidPW.enabled = false;
        marcusPW.enabled = false;
        playerNPCPW.enabled = false;

        // Position and rotate Player NPC
        playerNPC.transform.SetPositionAndRotation(
            playerNPCInCabinTarget.position,
            playerNPCInCabinTarget.rotation
        );
        // Position and rotate David
        david.transform.SetPositionAndRotation(
            davidInCabinTarget.position,
            davidInCabinTarget.rotation
        );
        // Position and rotate Marcus
        marcus.transform.SetPositionAndRotation(
            marcusInBedTarget.position,
            marcusInBedTarget.rotation
        );

        marcusAnim.SetTrigger("LayingInBedSick");
        davidAnim.SetTrigger("KneelDownInCabin");
        playerNPCAnim.SetTrigger("IdleInCabin");
    }

    public void StartDialogueOne()
    {
        Destroy(marcusInBedCam);
        dialogueOneCam.SetActive(true);
        dialogueOne.SetActive(true);
    }

    public void SwitchToTablePlayer()
    {
        player.SetActive(false);
        tablePlayer.SetActive(true);
        fpsGun.SetActive(false);
        // Move David to kitchen
        david.transform.SetPositionAndRotation(
            davidCookDinnerTarget.position,
            davidCookDinnerTarget.rotation
        );
        davidAnim.SetTrigger("IdleInKitchen");
        kitchenObjects.SetActive(true);
    }

    private void DisableGameObjects(IEnumerable<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);   
        }
    }

    private void RefreshDavidPathWalker()
    {
        davidPW.enabled = false;
        davidPW.enabled = true;
    }

    private void RefreshMarcusPathwalker()
    {
        marcusPW.enabled = false;
        marcusPW.enabled = true;
    }
}
