using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private GameObject npcPlayer;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cutsceneCameraOne;
    [SerializeField] private GameObject cutscenePath;
    [SerializeField] private GameObject reticle;

    private void OnTriggerEnter(Collider other)
    {
        cutscenePath.SetActive(true);
        player.SetActive(false);
        reticle.SetActive(false);
        npcPlayer.SetActive(true);
        cutsceneCameraOne.SetActive(true);
        Destroy(gameObject);
    }
}
