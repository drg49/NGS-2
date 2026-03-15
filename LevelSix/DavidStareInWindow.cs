using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidStareInWindow : MonoBehaviour
{
    // Kitchen light, boiling water audio, steam particles
    // Destroy these to enhance spooky atmosphere
    [SerializeField] private List<GameObject> gameObjectsToDestroy;
    [SerializeField] private AudioSource powerShutOff;
    [SerializeField] private AudioSource digitalBarkSong;
    [SerializeField] private List<GameObject> davidWaypointsToDeactivate;
    [SerializeField] private PathWalker davidPW;
    [SerializeField] private GameObject dialogueThree;

    private Collider triggerCollider;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("NPC"))
            return;

        // Destroy the collider so it can't fire again
        if (triggerCollider != null)
            Destroy(triggerCollider);

        StartCoroutine(WaitAndScare());
    }

    private IEnumerator WaitAndScare()
    {
        yield return new WaitForSeconds(6f);

        foreach (GameObject obj in gameObjectsToDestroy)
        {
            Destroy(obj);
        }

        // Clean up David's waypoints
        foreach (GameObject obj in davidWaypointsToDeactivate)
        {
            obj.SetActive(false);
        }
        // Always good practice to turn off pathwalkers when they are no longer being used
        // When we need the pathwalker again we can just set enabled to true
        davidPW.enabled = false;

        powerShutOff.Play();
        digitalBarkSong.Play();

        // Wait 5 more seconds after the song starts to start dialogue 3
        yield return new WaitForSeconds(5f);
        dialogueThree.SetActive(true);
    }
}
