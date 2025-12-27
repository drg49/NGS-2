using TMPro;
using UnityEngine;
using System.Collections;

public class FadePanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI interactionText;

    [Header("Input")]
    [SerializeField] private PlayerInputActions inputActions;

    [Header("Players")]
    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private GameObject showerPlayer;
    [SerializeField] private GameObject bathWater;

    [Header("Audio & Particles")]
    [SerializeField] private AudioSource showerAudio; // Your audio source
    [SerializeField] private ParticleSystem[] showerParticles; // Array for multiple particle systems

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

        // Activate and play all shower particle systems
        foreach (var ps in showerParticles)
        {
            ps.gameObject.SetActive(true);
            ps.Play();
        }

        // Play audio and stop particles after audio
        if (showerAudio != null)
        {
            showerAudio.Play();
            StartCoroutine(StopParticlesAfterAudio(showerAudio, showerParticles));
        }
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
    }
}
