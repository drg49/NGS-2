using UnityEngine;
using System.Collections;

public class ForestJumpscare : MonoBehaviour
{
    [SerializeField] private AudioSource aftermathSong;
    [SerializeField] private GameObject davidCreepyFPS;
    [SerializeField] private AudioSource davidJumpscareAudio;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject flashlightLight;

    [SerializeField] private float fadeDuration = 4f; // seconds for fade out

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Destroy all BoxColliders
        BoxCollider[] colliders = GetComponents<BoxCollider>();
        foreach (BoxCollider bc in colliders)
            Destroy(bc);

        // Start the jumpscare sequence
        StartCoroutine(JumpscareSequence());
    }

    private IEnumerator JumpscareSequence()
    {
        // Fade out the aftermath song
        float startVolume = aftermathSong.volume;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            aftermathSong.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        aftermathSong.volume = 0f;
        aftermathSong.Stop();

        // Wait 12 seconds before David jumpscare
        yield return new WaitForSeconds(12f);
        davidCreepyFPS.SetActive(true);
        davidJumpscareAudio.Play();
        flashlight.SetActive(false);
        flashlightLight.SetActive(false);
    }
}