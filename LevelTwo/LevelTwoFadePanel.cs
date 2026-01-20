using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoFadePanel : MonoBehaviour
{
    [SerializeField] private InstructionSequence firstInstruction;
    [SerializeField] private InstructionSequence restroomInstruction;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject fpsTray;
    [SerializeField] private GameObject sitDownTray;
    [SerializeField] private List<GameObject> sitDownTrayObjects;
    [SerializeField] private GameObject sitDownCamera;
    [SerializeField] protected InkDialogueManager dialogueManager;
    [SerializeField] protected TextAsset sitDownConvoJSON;
    [SerializeField] protected GameObject pianoSong;
    [SerializeField] protected GameObject ambienceManager;
    [SerializeField] protected GameObject creep;

    // Meet Marcus & Dave at Big Burger
    public void PlayFirstInstruction()
    {
        firstInstruction.Play();
    }

    public void SitDown()
    {
        player.SetActive(false);
        reticle.SetActive(false);
        sitDownTray.SetActive(true);
        Destroy(fpsTray);
        sitDownCamera.SetActive(true);
        ambienceManager.SetActive(false);
        pianoSong.SetActive(true);
        // This needs to be called here, otherwise NPC03 dialogue will call it early
        dialogueManager.OnDialogueFinished += HandleStoryEnd;
    }

    public void StartConvo()
    {
        dialogueManager.StartStory(sitDownConvoJSON);
        StartCoroutine(ActivateAfterDelay(8f));
    }

    private IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        creep.SetActive(true);
    }

    private void HandleStoryEnd()
    {
        dialogueManager.OnDialogueFinished -= HandleStoryEnd;
        // Start a coroutine to wait 2 seconds then log
        StartCoroutine(WaitThenLeave());
    }

    private IEnumerator WaitThenLeave()
    {
        yield return new WaitForSeconds(2f); // wait 2 seconds
        GetComponent<Animator>().SetTrigger("FadeLeaveConvo");
    }


    public void LeaveConvo()
    {
        Destroy(pianoSong);
        ambienceManager.SetActive(true);
        Destroy(sitDownCamera);
        player.SetActive(true);
        reticle.SetActive(true);

        // Destroy tray objects
        foreach (GameObject obj in sitDownTrayObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        // Clear the list so you don’t keep references to destroyed objects
        sitDownTrayObjects.Clear();
    }

    public void PlayRestroomInstruction()
    {
        restroomInstruction.Play();
    }
}
