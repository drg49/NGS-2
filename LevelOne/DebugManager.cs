using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public GameObject bedPlayer;
    public GameObject player;
    public GameObject fader;
    public GameObject showerPlayer;
    [SerializeField] private InkDialogueManager dialogueManager; // assign in inspector
    [SerializeField] private TextAsset marcusInkJSON;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bedPlayer.SetActive(false);
        player.SetActive(true);
        //player.SetActive(false);
        //showerPlayer.SetActive(true);
        // Start the Ink dialogue
        dialogueManager.OnDialogueFinished = ConvoDone;
        dialogueManager.StartStory(marcusInkJSON);
        fader.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConvoDone()
    {
        // Anything you want:
        // play animation
        // enable new interactables
        // trigger jump scare
        Debug.Log("Phone call ended");
    }
}
