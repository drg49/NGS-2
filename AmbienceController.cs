using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioClip outdoorAmbience;
    [SerializeField] private AudioClip indoorAmbience;

    [Header("Volume Settings")]
    [SerializeField] private float outdoorVolume = 0.7f;
    [SerializeField] private float indoorVolume = 0.5f;

    private AudioClip currentClip;

    private void Start()
    {
        // Start with outdoor ambience
        ambienceSource.clip = outdoorAmbience;
        ambienceSource.volume = outdoorVolume;
        ambienceSource.Play();
        currentClip = outdoorAmbience;
    }

    /// <summary>
    /// Call when player enters an indoor area
    /// </summary>
    public void EnterIndoor(Transform listenerTransform)
    {
        ambienceSource.transform.position = listenerTransform.position;
        PlayImmediate(indoorAmbience, indoorVolume);
    }

    /// <summary>
    /// Call when player exits indoor area
    /// </summary>
    public void ExitIndoor(Transform listenerTransform)
    {
        ambienceSource.transform.position = listenerTransform.position;
        PlayImmediate(outdoorAmbience, outdoorVolume);
    }

    /// <summary>
    /// Instantly switch to a new clip
    /// </summary>
    private void PlayImmediate(AudioClip newClip, float targetVolume)
    {
        if (newClip == null || currentClip == newClip) return;

        ambienceSource.Stop();
        ambienceSource.clip = newClip;
        ambienceSource.volume = targetVolume;
        ambienceSource.Play();

        currentClip = newClip;
    }
}
