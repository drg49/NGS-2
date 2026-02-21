using UnityEngine;
using System.Collections;

public class CameraSwitchAfterDelay : MonoBehaviour
{
    [SerializeField] private GameObject marcusTentCam;
    [SerializeField] private GameObject dialogueEight;
    [SerializeField] private float duration = 4f;

    private void OnEnable()
    {
        StartCoroutine(SwitchAfterDelay());
    }

    private IEnumerator SwitchAfterDelay()
    {
        yield return new WaitForSeconds(duration);

        marcusTentCam.SetActive(true);
        dialogueEight.SetActive(true);

        // Destroy this camera GameObject
        Destroy(gameObject);
    }
}