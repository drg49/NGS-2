using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public GameObject bedPlayer;
    public GameObject player;
    public GameObject fader;
    public GameObject showerPlayer;
    [SerializeField] private InkDialogueManager dialogueManager; // assign in inspector
    [SerializeField] private TextAsset marcusInkJSON;
    public GameObject cutsceneCamera;
    public GameObject cutscenePath;
    public GameObject npc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Delete settings
        PlayerPrefs.DeleteKey("LookSensitivity");
        PlayerPrefs.Save();
        bedPlayer.SetActive(false);
        player.SetActive(true);
        //player.SetActive(false);
        //showerPlayer.SetActive(true);
        // Start the Ink dialogue
        //dialogueManager.OnDialogueFinished = ConvoDone;
        //dialogueManager.StartStory(marcusInkJSON);
        fader.SetActive(false);
        //npc.SetActive(true);
        //cutscenePath.SetActive(true);
        //cutsceneCamera.SetActive(true);
    }
}
