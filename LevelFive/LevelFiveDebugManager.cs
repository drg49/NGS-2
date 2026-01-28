using UnityEngine;

public class LevelFiveDebugManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject carPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(player);
        carPlayer.SetActive(true);
    }
}
