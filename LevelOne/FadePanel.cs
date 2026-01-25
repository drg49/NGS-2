using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FadePanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private GameObject instructionText;

    [Header("Input")]
    [SerializeField] private PlayerInputActions inputActions;

    [Header("Game Objects")]
    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private GameObject npc;
    [SerializeField] private GameObject cutsceneCameraTwo;
    [SerializeField] private GameObject showerPlayer;
    [SerializeField] private GameObject bathWater;
    [SerializeField] private GameObject cellphoneOff;
    [SerializeField] private GameObject cellphoneOn;
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject doorInteract;
    [SerializeField] private GameObject tvOn;
    [SerializeField] private GameObject tvOff;

    [SerializeField] private Transform postCutsceneSpawn;

    [SerializeField] private InstructionSequence leaveHouseInstruction;

    [Header("Audio & Particles")]
    [SerializeField] private AudioSource showerAudio;
    [SerializeField] private ParticleSystem[] showerParticles;
    [SerializeField] private AudioSource ringtone;

    [Header("Level 3 Bed Cutscene")]
    [SerializeField] private GameObject bedPlayer;
    [SerializeField] private GameObject bedroomDarkLight;
    [SerializeField] private List<GameObject> objectsToDestroyInBed;
    [SerializeField] private AudioSource ghostWhisper;
    [SerializeField] private AudioSource ghostFootsteps;
    [SerializeField] private GameObject demonNPC;
    [SerializeField] private GameObject bedDemonNPC;
    [SerializeField] private AudioSource demonJumpscare;
    [SerializeField] private GameObject demonJumpscareImage;
    [SerializeField] private AudioSource scaredBreathing;
    [SerializeField] private GameObject scaredText;
    [SerializeField] private GameObject scaredTextTwo;
    [SerializeField] private TextMeshProUGUI fadeToLvlFourTxt;

    private void Awake()
    {
        inputActions ??= new PlayerInputActions();
    }

    public void DisplayInstructionalText()
    {
        if (SceneContext.CurrentLevelMode == LevelMode.LevelThree)
        {
            return;
        }
        string button = inputActions.Player.Interact.bindings[0].ToDisplayString();
        interactionText.text = $"Hold [{button}] to get out of bed";
    }

    public void SwitchPlayers()
    {
        mainPlayer.SetActive(false);
        showerPlayer.SetActive(true);
        bathWater.SetActive(true);
        cellphoneOff.SetActive(true);

        // Activate and play all shower particle systems
        foreach (var ps in showerParticles)
        {
            ps.gameObject.SetActive(true);
            ps.Play();
        }

        // Play audio and stop particles after audio
        
        showerAudio.Play();
        StartCoroutine(StopParticlesAfterAudio(showerAudio, showerParticles));
    }

    public void ExitBathtub()
    {
        Destroy(showerPlayer);
        Destroy(bathWater);
        mainPlayer.SetActive(true);
    }

    public void ExitCutscene()
    {
        npc.SetActive(false);
        mainPlayer.SetActive(true);
        Destroy(cutsceneCameraTwo);
        Destroy(tvOn);
        tvOff.SetActive(true);
        reticle.SetActive(true);
        var controller = mainPlayer.GetComponent<CharacterController>();
        controller.enabled = false;
        mainPlayer.transform.SetPositionAndRotation(
            postCutsceneSpawn.position,
            postCutsceneSpawn.rotation
        );
        controller.enabled = true;
        // Displays after 5 seconds
        leaveHouseInstruction.Play();
        // Activates after 5 seconds
        StartCoroutine(ActivateDoorExit());
    }

    private IEnumerator ActivateDoorExit()
    {
        yield return new WaitForSeconds(5f);
        doorInteract.SetActive(true);
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("SecondLevel_Street");
    }

    private IEnumerator StopParticlesAfterAudio(AudioSource audio, ParticleSystem[] particles)
    {
        // Wait until audio finishes
        yield return new WaitForSeconds(audio.clip.length);

        foreach (var ps in particles)
        {
            // Stop emitting new particles but let existing ones fade naturally
            ps.Stop(withChildren: false, ParticleSystemStopBehavior.StopEmitting);
        }

        // Wait for remaining particles to disappear
        float maxLifetime = 0f;
        foreach (var ps in particles)
            maxLifetime = Mathf.Max(maxLifetime, ps.main.startLifetime.constantMax);

        yield return new WaitForSeconds(maxLifetime);

        // Destroy all particle system GameObjects
        foreach (var ps in particles)
            Destroy(ps.gameObject);

        // Wait 2 seconds before showing the instruction text
        yield return new WaitForSeconds(2f);
        string pauseButton = inputActions.UI.Pause.bindings[0].ToDisplayString();

        InstructionTextAlphaFader fader = FindFirstObjectByType<InstructionTextAlphaFader>();
        fader.Show($"You can press [{pauseButton}] at any time to view objectives in the pause menu.", 6.5f);

        // A few seconds after the fade duration finishes
        yield return new WaitForSeconds(8f);

        cellphoneOff.SetActive(false);
        cellphoneOn.SetActive(true);
        ringtone.Play();
    }

    public void LvlThreeGetInBed()
    {
        TMP_Text text = instructionText.GetComponent<TMP_Text>();
        text.text = "";
        mainPlayer.SetActive(false);
        bedPlayer.SetActive(true);
        bedroomDarkLight.SetActive(true);
        // Turn off lights
        foreach (GameObject obj in objectsToDestroyInBed)
        {
            Destroy(obj);
        }
        RenderSettings.skybox = null;

        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = Color.black;

        RenderSettings.reflectionIntensity = 0f;
    }

    public void PlayGhostWhisper()
    {
        ghostWhisper.Play();
    }

    public void PlayGhostFootsteps()
    {
        ghostFootsteps.Play();
    }

    public void ShowDemon()
    {
        demonNPC.SetActive(true);
        // Lower light intensity
        Light darkLightIntensity = bedroomDarkLight.GetComponent<Light>();
        darkLightIntensity.intensity = 0.2f;
    }

    public void SwitchDemons()
    {
        Destroy(demonNPC);
        Light darkLightIntensity = bedroomDarkLight.GetComponent<Light>();
        darkLightIntensity.intensity = 0.4f;
        bedDemonNPC.SetActive(true);
    }

    public void DemonJumpscare()
    {
        demonJumpscare.Play();
        demonJumpscareImage.SetActive(true);
    }

    public void DestroyDemonJumpscare()
    {
        Destroy(demonJumpscareImage);
        Destroy(bedDemonNPC);
    }

    public void HeavyBreathing()
    {
        scaredBreathing.Play();
    }

    public void ShowScaredTextOne()
    {
        scaredText.SetActive(true);
    }

    public void ShowScaredTextTwo()
    {
        scaredTextTwo.SetActive(true);
    }

    public void ShowLastTextForLvl3()
    {
        StartCoroutine(FadeTextSequence());
    }

    private readonly float fadeDuration = 1f;
    private readonly float displayTime = 12f;

    private IEnumerator FadeTextSequence()
    {
        // Step 1: Wait before starting fade
        yield return new WaitForSeconds(1f);

        // Step 2: Ensure text is active and fully transparent
        fadeToLvlFourTxt.gameObject.SetActive(true);
        Color c = fadeToLvlFourTxt.color;
        fadeToLvlFourTxt.color = new Color(c.r, c.g, c.b, 0f);

        // Step 3: Fade in
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            fadeToLvlFourTxt.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeToLvlFourTxt.color = new Color(c.r, c.g, c.b, 1f);

        // Step 4: Keep text fully visible
        yield return new WaitForSeconds(displayTime);

        // Step 5: Fade out
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            fadeToLvlFourTxt.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeToLvlFourTxt.color = new Color(c.r, c.g, c.b, 0f);
        fadeToLvlFourTxt.gameObject.SetActive(false);

        // Go to next scene
        SceneManager.LoadScene("ThirdLevel_OnTheRoad");
    }
}
