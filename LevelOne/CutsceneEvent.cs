using UnityEngine;
using UnityEngine.Video;

public class CutsceneEvent : MonoBehaviour
{
    [SerializeField] private VideoPlayer tvVideoPlayer;
    [SerializeField] private GameObject handRemote;
    [SerializeField] private GameObject tableRemote;
    [SerializeField] private AudioSource tvSwitchAudio;
    [SerializeField] private AudioSource song;
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset newsInkJSON;

    public void TurnOnTelevision()
    {
        tvSwitchAudio.Play();
        tvVideoPlayer.Play();
        dialogueManager.OnDialogueFinished = OnCutsceneEnd;
        // Start the Ink dialogue
        dialogueManager.StartStory(newsInkJSON);
        song.Play();
    }

    public void GrabRemote()
    {
        tableRemote.SetActive(false);
        handRemote.SetActive(true);
    }

    private void OnCutsceneEnd()
    {
        Debug.Log("Cutscene end");
    }
}
