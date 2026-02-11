using System.Collections;
using UnityEngine;

public class WineObjective : MonoBehaviour
{
    [SerializeField] private GameObject wine;
    [SerializeField] private GameObject demonWaypoints;

    void Start()
    {
        StartCoroutine(ActivateRoutine());
    }

    private IEnumerator ActivateRoutine()
    {
        yield return new WaitForSeconds(3f);
        wine.SetActive(true);
        demonWaypoints.SetActive(true);
    }
}
