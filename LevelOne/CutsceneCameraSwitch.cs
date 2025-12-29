using UnityEngine;
using System.Collections;

public class CutsceneCameraSwitch : MonoBehaviour
{
    [Header("Camera References")]
    [SerializeField] private GameObject cutsceneCameraOne;
    [SerializeField] private GameObject cutsceneCameraTwo;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("NPC")) return;

        Destroy(cutsceneCameraOne);
        cutsceneCameraTwo.SetActive(true);

        GetComponent<Collider>().enabled = false;
    }

}
