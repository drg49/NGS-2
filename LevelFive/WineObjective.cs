using System.Collections;
using UnityEngine;

public class WineObjective : MonoBehaviour
{
    [SerializeField] private GameObject wine;

    void Start()
    {
        StartCoroutine(ActivateRoutine());
    }

    private IEnumerator ActivateRoutine()
    {
        yield return new WaitForSeconds(3f);
        wine.SetActive(true);
    }
}
