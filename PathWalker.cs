using UnityEngine;
using System.Collections.Generic;

public class PathWalker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    [Header("Path Settings")]
    [SerializeField] private Transform waypointsParent;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private bool loopPath = false;

    [Header("Footsteps")]
    [SerializeField] private AudioClip[] footstepAudioClips;
    [SerializeField] private float footstepInterval = 0.5f;

    private readonly List<Transform> waypoints = new();
    private int currentWaypoint;
    private bool isMoving;
    private float footstepTimer;

    void OnEnable()
    {
        BuildWaypointList();
        StartWalking();
    }

    // If inactive waypoint targets are enabled later on, this script will need to be refreshed.
    private void BuildWaypointList()
    {
        waypoints.Clear();

        if (waypointsParent == null)
            return;

        foreach (Transform child in waypointsParent)
        {
            if (child.gameObject.activeInHierarchy)
                waypoints.Add(child);
        }

        currentWaypoint = 0;
    }

    private void StartWalking()
    {
        if (waypoints.Count == 0)
        {
            isMoving = false;
            return;
        }

        isMoving = true;

        if (animator != null)
            animator.SetBool("IsMoving", true);
    }

    void Update()
    {
        if (!isMoving || waypoints.Count == 0)
            return;

        Transform target = waypoints[currentWaypoint];
        Vector3 targetPosition = target.position;

        Move(targetPosition);
        HandleFootsteps();

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            AdvanceWaypoint();
        }
    }

    private void Move(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                rotateSpeed * Time.deltaTime
            );
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    private void AdvanceWaypoint()
    {
        if (currentWaypoint < waypoints.Count - 1)
        {
            currentWaypoint++;
        }
        else if (loopPath)
        {
            currentWaypoint = 0;
        }
        else
        {
            StopWalking();
        }
    }

    private void StopWalking()
    {
        isMoving = false;
        footstepTimer = 0f;

        if (animator != null)
            animator.SetBool("IsMoving", false);
    }

    private void HandleFootsteps()
    {
        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0f && footstepAudioClips.Length > 0)
        {
            int index = Random.Range(0, footstepAudioClips.Length);
            audioSource.PlayOneShot(footstepAudioClips[index]);
            footstepTimer = footstepInterval;
        }
    }
}
