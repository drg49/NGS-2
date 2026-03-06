using UnityEngine;

public class LevelSixDebugManager : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private GameObject preventCabinExploreCollider;
    [SerializeField] private GameObject foundCabinTrigger;
    [SerializeField] private GameObject notifyDavid;

    public float debugRunSpeed = 8;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fpsController.runSpeed = debugRunSpeed;
        fpsController.canRun = true;
        notifyDavid.SetActive(true);
        Destroy(preventCabinExploreCollider);
        Destroy(foundCabinTrigger);
    }
}
