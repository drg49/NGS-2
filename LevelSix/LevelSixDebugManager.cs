using UnityEngine;

public class LevelSixDebugManager : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fpsController.runSpeed = 10;
    }
}
