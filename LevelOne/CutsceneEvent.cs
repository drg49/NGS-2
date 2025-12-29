using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneEvent : MonoBehaviour
{
    [SerializeField] private VideoPlayer tvVideoPlayer;
    [SerializeField] private GameObject handRemote;
    [SerializeField] private GameObject reticle;
    [SerializeField] private GameObject tableRemote;
    [SerializeField] private AudioSource tvSwitchAudio;
    [SerializeField] private AudioSource song;
    [SerializeField] private InkDialogueManager dialogueManager;
    [SerializeField] private TextAsset newsInkJSON;
    [SerializeField] private Animator fadeAnimator;

    public void TurnOnTelevision()
    {
        tvSwitchAudio.Play();
        tvVideoPlayer.Play();
        dialogueManager.OnDialogueFinished = OnCutsceneEnd;
        // Start the Ink dialogue
        dialogueManager.StartStory(newsInkJSON);
        // Start coroutine to play song after 1 second
        StartCoroutine(PlaySongDelayed(1f));
    }

    private IEnumerator PlaySongDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        song.Play();
    }

    public void GrabRemote()
    {
        tableRemote.SetActive(false);
        handRemote.SetActive(true);
    }

    private void OnCutsceneEnd()
    {
        reticle.SetActive(false);
        fadeAnimator.SetTrigger("FadeInOutPostCutscene");
    }
}
