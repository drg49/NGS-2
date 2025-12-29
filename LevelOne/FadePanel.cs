using TMPro;
using UnityEngine;
using System.Collections;

public class FadePanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI interactionText;

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

    [SerializeField] private Transform postCutsceneSpawn;

    public RenderTexture renderTexture;

    [Header("Audio & Particles")]
    [SerializeField] private AudioSource showerAudio;
    [SerializeField] private ParticleSystem[] showerParticles;
    [SerializeField] private AudioSource ringtone;

    private void Awake()
    {
        inputActions ??= new PlayerInputActions();
    }

    public void DisplayInstructionalText()
    {
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
        showerPlayer.SetActive(false);
        mainPlayer.SetActive(true);
        Destroy(bathWater);
    }

    public void ExitCutscene()
    {
        npc.SetActive(false);
        mainPlayer.SetActive(true);
        Destroy(cutsceneCameraTwo);
        reticle.SetActive(true);
        var controller = mainPlayer.GetComponent<CharacterController>();
        controller.enabled = false;
        mainPlayer.transform.SetPositionAndRotation(
            postCutsceneSpawn.position,
            postCutsceneSpawn.rotation
        );
        controller.enabled = true;
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = activeRT;
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
}
