using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public GameObject bedPlayer;
    public GameObject player;
    public GameObject fader;
    public GameObject showerPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bedPlayer.SetActive(false);
        player.SetActive(true);
        //player.SetActive(false);
        //showerPlayer.SetActive(true);
        //fader.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
