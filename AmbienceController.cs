using UnityEngine;
using System.Collections;

public class AmbienceController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioClip outdoorAmbience;
    [SerializeField] private AudioClip indoorAmbience;

    [Header("Volume Settings")]
    [SerializeField] private float outdoorVolume = 0.7f;
    [SerializeField] private float indoorVolume = 0.5f;

    [Header("Fade Settings")]
    [SerializeField] private float fadeTime = 1.5f;

    private Coroutine fadeRoutine;
    private AudioClip currentClip;

    private void Start()
    {
        // Play outdoor ambience by default
        Play(outdoorAmbience);
    }

    /// <summary>
    /// Call when player enters an indoor area
    /// </summary>
    public void EnterIndoor()
    {
        Play(indoorAmbience);
    }

    /// <summary>
    /// Call when player exits indoor area
    /// </summary>
    public void ExitIndoor()
    {
        Play(outdoorAmbience);
    }

    /// <summary>
    /// Handles starting a fade to a new clip
    /// </summary>
    private void Play(AudioClip newClip)
    {
        if (newClip == null || currentClip == newClip)
            return;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(Fade(newClip));
    }

    /// <summary>
    /// Smoothly fades out the current clip, switches, and fades in
    /// </summary>
    private IEnumerator Fade(AudioClip newClip)
    {
        float startVolume = ambienceSource.volume;

        // Fade out
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            ambienceSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeTime);
            yield return null;
        }

        ambienceSource.Stop();
        ambienceSource.clip = newClip;
        ambienceSource.Play();

        // Determine target volume based on clip
        float targetVolume = (newClip == indoorAmbience) ? indoorVolume : outdoorVolume;

        // Fade in
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            ambienceSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeTime);
            yield return null;
        }

        ambienceSource.volume = targetVolume;
        currentClip = newClip;
    }
}
