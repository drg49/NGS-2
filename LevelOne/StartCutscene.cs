using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject npcPlayer;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cutsceneCameraOne;
    [SerializeField] private GameObject reticle;

    private void OnTriggerEnter(Collider other)
    {
        player.SetActive(false);
        reticle.SetActive(false);
        npcPlayer.SetActive(true);
        cutsceneCameraOne.SetActive(true);
    }
}
