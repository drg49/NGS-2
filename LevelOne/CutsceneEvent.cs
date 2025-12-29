using UnityEngine;
using UnityEngine.Video;

public class CutsceneEvent : MonoBehaviour
{
    [SerializeField] private VideoPlayer tvVideoPlayer;
    [SerializeField] private GameObject handRemote;
    [SerializeField] private GameObject tableRemote;
    [SerializeField] private AudioSource tvSwitchAudio;

    public void TurnOnTelevision()
    {
        tvSwitchAudio.Play();
        tvVideoPlayer.Play();
    }

    public void GrabRemote()
    {
        tableRemote.SetActive(false);
        handRemote.SetActive(true);
    }
}
