using UnityEngine;
using System.Collections;

public class ForestJumpscare : MonoBehaviour
{
    [SerializeField] private AudioSource aftermathSong;
    [SerializeField] private AudioSource grudgeJumpscareAudio;
    [SerializeField] private GameObject grudgeJumpscareImage;

    [SerializeField] private float fadeDuration = 2f; // seconds for fade out

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Call Jumpscare in Forest");

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

        // Wait 7 seconds
        yield return new WaitForSeconds(7f);

        // Play grudge audio
        grudgeJumpscareAudio.Play();

        // Flash grudge image
        grudgeJumpscareImage.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        grudgeJumpscareImage.SetActive(false);
    }
}