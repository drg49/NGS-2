using UnityEngine;

public class LevelSixDebugManager : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private GameObject preventCabinExploreCollider;
    [SerializeField] private GameObject foundCabinTrigger;

    public float debugRunSpeed = 8;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fpsController.runSpeed = debugRunSpeed;
        fpsController.canRun = true;
        Destroy(preventCabinExploreCollider);
        Destroy(foundCabinTrigger);
    }
}
