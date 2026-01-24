using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset sitDownConvoJSON;
    [SerializeField] private GameObject pianoSong;
    [SerializeField] private GameObject ambienceManager;
    [SerializeField] private GameObject creep;
    [SerializeField] private GameObject npcSeven;
    [SerializeField] private GameObject toiletInteract;
    [SerializeField] private GameObject toiletPlayer;
    [SerializeField] private TextMeshProUGUI fadeToLvlThreeTxt;

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
        npcSeven.SetActive(true);

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
        toiletInteract.SetActive(true);
    }

    public void PlayRestroomInstruction()
    {
        restroomInstruction.Play();
    }

    public void LeaveToilet()
    {
        Destroy(toiletPlayer);
        player.SetActive(true);
    }

    private float fadeDuration = 1f;            // How long the fade lasts
    private float displayTime = 3.5f;

    public void DisablePlayerAndShowText()
    {
        // Disable player movement
        FirstPersonController controller = player.GetComponent<FirstPersonController>();
        if (controller != null)
            controller.enabled = false;

        // Start the coroutine
        StartCoroutine(FadeTextSequence());
    }

    private IEnumerator FadeTextSequence()
    {
        // Step 1: Wait before starting fade
        yield return new WaitForSeconds(1f);

        // Step 2: Ensure text is active and fully transparent
        fadeToLvlThreeTxt.gameObject.SetActive(true);
        Color c = fadeToLvlThreeTxt.color;
        fadeToLvlThreeTxt.color = new Color(c.r, c.g, c.b, 0f);

        // Step 3: Fade in
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            fadeToLvlThreeTxt.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeToLvlThreeTxt.color = new Color(c.r, c.g, c.b, 1f);

        // Step 4: Keep text fully visible
        yield return new WaitForSeconds(displayTime);

        // Step 5: Fade out
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            fadeToLvlThreeTxt.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeToLvlThreeTxt.color = new Color(c.r, c.g, c.b, 0f);
        fadeToLvlThreeTxt.gameObject.SetActive(false);

        // Step 6: Call level transition
        GoToLevelThree();
    }

    public void GoToLevelThree()
    {
        Debug.Log("Going to level three");
        // SceneManager.LoadScene("Level3"); // Uncomment when ready
    }
}
