using System.Collections;
using TMPro;
using UnityEngine;

public class LevelFiveFadePanel : MonoBehaviour
{
    // ===== Car =====
    [Header("Car")]
    [SerializeField] private GameObject carColliders;
    [SerializeField] private GameObject car;
    [SerializeField] private GameObject parkedCar;

    // ===== Characters =====
    [Header("Characters")]
    [SerializeField] private GameObject marcus;
    [SerializeField] private Animator marcusAnim;
    [SerializeField] private GameObject david;
    [SerializeField] private Animator davidAnim;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerNPC;

    // ===== Dialogue =====
    [Header("Dialogue")]
    [SerializeField] private GameObject dialogueOneCam;
    [SerializeField] private GameObject dialogueOne;
    [SerializeField] private GameObject dialogueTwo;
    [SerializeField] private TextMeshProUGUI transitionText;

    // ===== Targets =====
    [Header("Targets")]
    [SerializeField] private Transform playerTentTarget;
    [SerializeField] private Transform davidTentTarget;
    [SerializeField] private Transform marcusTentTarget;
    [SerializeField] private Transform davidFireTarget;
    [SerializeField] private Transform marcusFireTarget;

    // ===== Objectives =====
    [Header("Objectives")]
    [SerializeField] private GameObject logObjective;

    // ===== Misc =====
    [Header("Misc")]
    [SerializeField] private GameObject marcusTent;
    [SerializeField] private GameObject davidTent;
    [SerializeField] private GameObject playerTent;
    [SerializeField] private GameObject marcusBag;
    [SerializeField] private GameObject davidBag;
    [SerializeField] private AudioSource ambience;
    [SerializeField] private AudioSource ambienceNight;
    [SerializeField] private AudioSource matchStrikeAudio;
    [SerializeField] private AudioSource firepitAudio;
    [SerializeField] private GameObject nightCutsceneCamOne;
    [SerializeField] private GameObject nightCutsceneCamTwo;
    [SerializeField] private Material nightSkybox;
    [SerializeField] private Light directionalLight;
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject flamesEffect;
    [SerializeField] private GameObject logFromPileOne;
    [SerializeField] private GameObject logFromPileTwo;

    public void EnterCampsite()
    {
        Destroy(carColliders);
        Destroy(car);
        ambience.Play();
        parkedCar.SetActive(true);
        marcus.SetActive(true);
        david.SetActive(true);
        // Turn On Camera
        dialogueOneCam.SetActive(true);
    }

    public void StartDialogueOne()
    {
        dialogueOne.SetActive(true);
    }

    public void SetUpTent()
    {
        marcusTent.SetActive(true);
        davidTent.SetActive(true);
        playerTent.SetActive(true);
        // Make sure player is not inside tent after activation
        player.transform.position = playerTentTarget.position;
        marcusAnim.SetTrigger("IdleAfterTentSetUp");
        davidAnim.SetTrigger("IdleAfterTentSetUp");
        // Position and rotate NPCs
        david.transform.SetPositionAndRotation(
            davidTentTarget.position,
            davidTentTarget.rotation
        );
        marcus.transform.SetPositionAndRotation(
            marcusTentTarget.position,
            marcusTentTarget.rotation
        );
        // Destroy the items in their hands
        Destroy(marcusBag);
        Destroy(davidBag);
    }

    public void ShowLogObjective()
    {
        logObjective.SetActive(true);
    }

    public void StartFireAndDisablePlayer()
    {
        player.SetActive(false);
        reticle.SetActive(false);
        nightCutsceneCamOne.SetActive(true);
        matchStrikeAudio.Play();
        david.transform.SetPositionAndRotation(
            davidFireTarget.position,
            davidFireTarget.rotation
        );
        marcus.transform.SetPositionAndRotation(
            marcusFireTarget.position,
            marcusFireTarget.rotation
        );
        marcusAnim.SetTrigger("LayingNearFire");
        davidAnim.SetTrigger("SittingNearFire");
        playerNPC.SetActive(true);
        flamesEffect.SetActive(true);
    }

    public void FireSoundsAndTransitionTxt()
    {
        firepitAudio.Play();
        SetNight();
        Destroy(logFromPileOne);
        Destroy(logFromPileTwo);
        StartCoroutine(FadeTextSequence());
    }

    private readonly float fadeDuration = 2f;
    private readonly float displayTime = 4.5f;

    private IEnumerator FadeTextSequence()
    {
        // Step 1: Wait before starting fade
        yield return new WaitForSeconds(1f);

        // Step 2: Ensure text is active and fully transparent
        transitionText.gameObject.SetActive(true);
        Color c = transitionText.color;
        transitionText.color = new Color(c.r, c.g, c.b, 0f);

        // Step 3: Fade in
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            transitionText.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        transitionText.color = new Color(c.r, c.g, c.b, 1f);

        // Step 4: Keep text fully visible
        yield return new WaitForSeconds(displayTime);

        // Step 5: Fade out
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            transitionText.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        transitionText.color = new Color(c.r, c.g, c.b, 0f);
        transitionText.gameObject.SetActive(false);

        // call any special functions here afterwards
    }

    // Change skybox to night
    private void SetNight()
    {
        ambience.Stop();
        ambienceNight.Play();
        RenderSettings.skybox = nightSkybox;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogDensity = 0.02f;

        directionalLight.intensity = 0.5f;
        directionalLight.color = new Color(0.6f, 0.7f, 1f); // soft blue moonlight

        DynamicGI.UpdateEnvironment(); // important!
    }

    public void SwitchToNightCamTwo()
    {
        Destroy(nightCutsceneCamOne);
        nightCutsceneCamTwo.SetActive(true);
    }

    public void StartDialogueTwo()
    {
        dialogueTwo.SetActive(true);
    }
}
