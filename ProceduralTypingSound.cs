using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProceduralTypingSound : MonoBehaviour
{
    public float duration = 0.05f;
    public float baseFrequency = 180f; // Slightly lower for smoother tone
    public float volume = 0.1f;       // Lower volume to reduce harshness
    private AudioSource audioSource;
    private bool playSound = false;
    private int sampleCountRemaining;
    private float sampleRate;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        sampleRate = AudioSettings.outputSampleRate;
    }

    public void PlaySound()
    {
        playSound = true;
        sampleCountRemaining = Mathf.CeilToInt(duration * sampleRate);
        audioSource.Play();
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (!playSound || sampleCountRemaining <= 0)
            return;

        for (int i = 0; i < data.Length; i += channels)
        {
            if (sampleCountRemaining <= 0)
            {
                playSound = false;
                break;
            }

            float t = sampleCountRemaining / sampleRate;

            // Smooth fade-in and fade-out using Hanning window
            float envelope = Mathf.Sin(Mathf.PI * (1f - t / duration) * 0.5f);
            envelope *= envelope; // sin² curve for smooth attack/release

            float sample = Mathf.Sin(2f * Mathf.PI * baseFrequency * t) * envelope;

            for (int c = 0; c < channels; c++)
                data[i + c] += sample * volume;

            sampleCountRemaining--;
        }
    }
}
