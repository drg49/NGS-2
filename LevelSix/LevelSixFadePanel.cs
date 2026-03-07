using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSixFadePanel : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource aftermathSong;

    [Header("Cameras")]
    [SerializeField] private GameObject notifyDavidCam;
    [SerializeField] private GameObject carryMarcusCam;

    [Header("Characters")]
    [SerializeField] private GameObject marcus;
    [SerializeField] private Animator marcusAnim;
    [SerializeField] private PathWalker marcusPW;
    [SerializeField] private GameObject david;
    [SerializeField] private Animator davidAnim;
    [SerializeField] private PathWalker davidPW;
    [SerializeField] private GameObject player; //
    [SerializeField] private GameObject playerNPC;
    [SerializeField] private Animator playerNPCAnim;
    [SerializeField] private PathWalker playerNPCPW;

    [Header("Targets")]
    [SerializeField] private Transform playerNPCCarryMarcusTarget;
    [SerializeField] private Transform davidCarryMarcusTarget;
    [SerializeField] private Transform marcusBeingCarriedTarget;
    [SerializeField] private List<GameObject> carryPathWaypointsToDisable;
    // Enter cabin targets
    [SerializeField] private GameObject marcusWP3;
    [SerializeField] private GameObject davidWP4;



    // David and Player NPC carry Marcus into Cabin
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

        // Fade out aftermath song
        StartCoroutine(FadeOutSong());
    }

    public void ActivateCarryWaypoints()
    {
        // First path target for Player NPC, so just enable it's script
        playerNPCPW.enabled = true;

        // Disable waypoints previous to this 'Carry Walk' path
        foreach (GameObject waypoint in carryPathWaypointsToDisable)
        {
            waypoint.SetActive(false);
        }

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

    private IEnumerator FadeOutSong()
    {
        float fadeDuration = 4f;
        float startVolume = aftermathSong.volume;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            aftermathSong.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        aftermathSong.volume = 0f;
        aftermathSong.Stop();
    }
}
