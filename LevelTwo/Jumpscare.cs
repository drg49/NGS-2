using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject freak;
    [SerializeField] private GameObject freakCamera;
    [SerializeField] private GameObject leaveRestaurantTrigger;
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset freakInkJson;
    [SerializeField] private AudioSource jumpscareAudio;


    private void OnTriggerEnter(Collider other)
    {
        // Optional: only trigger on player
        if (!other.CompareTag("Player"))
            return;

        player.SetActive(false);
        freak.SetActive(true);
        freakCamera.SetActive(true);
        jumpscareAudio.Play();
        dialogueManager.OnDialogueFinished = OnDialogueEnd;
        // Start the Ink dialogue
        dialogueManager.StartStory(freakInkJson);
        Destroy(GetComponent<Collider>());
    }

    private void OnDialogueEnd()
    {
        freakCamera.SetActive(false);
        player.SetActive(true);
        leaveRestaurantTrigger.SetActive(true);
    }
}
