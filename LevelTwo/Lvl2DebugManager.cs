using UnityEngine;

public class Lvl2DebugManager : MonoBehaviour
{
    [SerializeField] protected GameObject sitDownInteract;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sitDownInteract.SetActive(true);
    }
}
