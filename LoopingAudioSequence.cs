using System.Collections;
using UnityEngine;

public class LoopingAudioSequence : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float delayBetweenClips = 2f;

    private Coroutine loopCoroutine;

    private void OnEnable()
    {
        if (clips.Length > 0 && audioSource != null)
        {
            loopCoroutine = StartCoroutine(PlayLoop());
        }
    }

    private void OnDisable()
    {
        if (loopCoroutine != null)
        {
            StopCoroutine(loopCoroutine);
        }
    }

    private IEnumerator PlayLoop()
    {
        int index = 0;

        while (true)
        {
            // Play current clip
            audioSource.clip = clips[index];
            audioSource.Play();

            // Wait for clip to finish
            yield return new WaitForSeconds(audioSource.clip.length);

            // Wait additional delay
            yield return new WaitForSeconds(delayBetweenClips);

            // Move to next clip (loop back to start)
            index = (index + 1) % clips.Length;
        }
    }
}