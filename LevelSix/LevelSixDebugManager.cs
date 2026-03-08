using UnityEngine;

public class LevelSixDebugManager : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private GameObject preventCabinExploreCollider;
    [SerializeField] private GameObject foundCabinTrigger;
    [SerializeField] private GameObject notifyDavid;
    [SerializeField] private PathWalker davidPW;
    [SerializeField] private PathWalker marcusPW;

    [SerializeField] private float debugRunSpeed = 8f;

    void Start()
    {
        ApplyDebugSpeed();
        //notifyDavid.SetActive(true);
    }

    void OnValidate()
    {
        // Runs whenever a value changes in the inspector
        ApplyDebugSpeed();
    }

    private void ApplyDebugSpeed()
    {
        if (fpsController != null)
        {
            fpsController.runSpeed = debugRunSpeed;
            fpsController.canRun = true;
        }

        if (davidPW != null)
        {
            davidPW.SetMoveSpeed(debugRunSpeed);
        }

        if (marcusPW != null)
        {
            marcusPW.SetMoveSpeed(debugRunSpeed);
        }
    }
}
