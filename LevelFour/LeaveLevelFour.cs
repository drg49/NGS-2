using UnityEngine;
using System.Collections;

public class LeaveLevelFour : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private float fadeDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        fadeAnimator.SetTrigger("LeaveLvlFour");
        StartCoroutine(FadeOutAudio(engineAudio, fadeDuration));
    }

    private IEnumerator FadeOutAudio(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
