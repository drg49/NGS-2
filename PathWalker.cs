using UnityEngine;
using System.Collections.Generic;

public class PathWalker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("Path Settings")]
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private bool loopPath = false; // Toggle looping

    private int currentWaypoint = 0;
    private bool isMoving = true;

    void Update()
    {
        if (!isMoving || waypoints.Count == 0) return;

        Transform target = waypoints[currentWaypoint];
        Vector3 targetPosition = target.position;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Rotate smoothly toward target
        if (moveDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        }

        // Move toward waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if reached
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < 0.05f)
        {
            if (currentWaypoint < waypoints.Count - 1)
            {
                currentWaypoint++;
            }
            else
            {
                if (loopPath)
                {
                    // Restart from first waypoint
                    currentWaypoint = 0;
                }
                else
                {
                    // Stop completely at last waypoint
                    animator.SetBool("IsMoving", false);
                    isMoving = false;
                }
            }
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
    }
}
