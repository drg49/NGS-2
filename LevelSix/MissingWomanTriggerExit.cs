using UnityEngine;

public class MissingWomanTriggerExit : MonoBehaviour
{
    [SerializeField] private GameObject missingWomanAudio;
    [SerializeField] private GameObject missingWomanObjective;

    private void OnTriggerExit()
    {
        missingWomanAudio.SetActive(true);
        missingWomanObjective.SetActive(true);
    }
}
