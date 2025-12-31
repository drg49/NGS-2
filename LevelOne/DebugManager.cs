using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        bedPlayer.SetActive(false);
        player.SetActive(true);
        //StartCoroutine(WaitAndLoad());
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

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("SecondLevel_Street");
    }
}
