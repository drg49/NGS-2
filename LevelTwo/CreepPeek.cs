using UnityEngine;
using System.Collections;

public class CreepPeek : MonoBehaviour
{
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float waitTime = 15f; // Time to wait at target1

    private void Start()
    {
        StartCoroutine(MoveSequence());
    }

    private IEnumerator MoveSequence()
    {
        // Move to target1
        yield return StartCoroutine(MoveToTarget(target1.position));

        // Wait for 15 seconds at target1
        yield return new WaitForSeconds(waitTime);

        // Move back to target2
        yield return StartCoroutine(MoveToTarget(target2.position));

        // Destroy this GameObject
        Destroy(gameObject);
    }

    private IEnumerator MoveToTarget(Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination; // Ensure exact final position
    }
}
