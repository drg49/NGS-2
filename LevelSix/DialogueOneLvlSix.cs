using UnityEngine;
using System.Collections;

public class DialogueOneLvlSix : MonoBehaviour
{
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset dialogueOneInkJSON;
    [SerializeField] private GameObject dialogueOneCam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerNPC;
    [SerializeField] private Transform playerReadyToHuntTarget;
    [SerializeField] private BoxCollider gunCollider;
    [SerializeField] private AudioSource aftermathSong;
    [SerializeField] private Material afternoonSkybox;
    [SerializeField] private Light directionalLight;
    [SerializeField] private GameObject huntObjective;

    void OnEnable()
    {
        dialogueManager.OnDialogueFinished += EndDialogue;
    }

    void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= EndDialogue;
    }

    void Start()
    {
        dialogueManager.StartStory(dialogueOneInkJSON);
    }

    private void EndDialogue()
    {
        player.transform.SetPositionAndRotation(
            playerReadyToHuntTarget.position,
            playerReadyToHuntTarget.rotation
        );
        Destroy(dialogueOneCam);
        player.SetActive(true);
        playerNPC.SetActive(false);
        // Player can now pick up the gun
        gunCollider.enabled = true;
        SetAfternoon();
        StartCoroutine(FadeOutSong());
        huntObjective.SetActive(true);
    }

    // Fade out aftermath song
    private IEnumerator FadeOutSong()
    {
        float fadeDuration = 4f;
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
        Destroy(gameObject);
    }

    // Change skybox to night
    private void SetAfternoon()
    {
        RenderSettings.skybox = afternoonSkybox;

        directionalLight.intensity = 0.9f;

        // warm orange sunlight
        directionalLight.color = new Color(1.0f, 0.6f, 0.3f);

        DynamicGI.UpdateEnvironment(); // refresh global illumination
    }
}
