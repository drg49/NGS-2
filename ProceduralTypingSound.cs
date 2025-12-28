using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProceduralTypingSound : MonoBehaviour
{
    public float duration = 0.05f;     // Length of each blip
    public float baseFrequency = 200f; // Lower pitch for deeper sound
    public float volume = 0.2f;
    private AudioSource audioSource;
    private bool playSound = false;
    private int sampleCountRemaining;
    private float sampleRate;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound
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

            // Generate a deeper sine wave
            float t = sampleCountRemaining / sampleRate;
            // Short exponential decay to make it “pluck-like”
            float envelope = Mathf.Exp(-20f * (1f - t / duration));
            float sample = Mathf.Sin(2f * Mathf.PI * baseFrequency * t) * envelope;

            for (int c = 0; c < channels; c++)
                data[i + c] += sample * volume;

            sampleCountRemaining--;
        }
    }
}
