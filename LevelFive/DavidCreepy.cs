using UnityEngine;

public class DavidCreepy : MonoBehaviour
{
    [SerializeField] private GameObject wakeUpMarcusObjective;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject flashlightLight;

    // Called when grab animation ends
    public void DestroyDavidCreepy()
    {
        wakeUpMarcusObjective.SetActive(true);
        flashlight.SetActive(true);
        flashlightLight.SetActive(true);
        Destroy(gameObject);
    }
}
