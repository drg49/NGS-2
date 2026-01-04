using UnityEngine;
using System.Collections.Generic;

public class PathWalker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    [Header("Path Settings")]
    [SerializeField] private Transform waypointsParent; // Assign the parent object
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private bool loopPath = false;

    [Header("Footsteps")]
    [SerializeField] private AudioClip[] footstepAudioClips;
    [SerializeField] private float footstepInterval = 0.5f;

    private List<Transform> waypoints = new List<Transform>(); // now private
    private int currentWaypoint = 0;
    private bool isMoving = true;
    private float footstepTimer;

    void Awake()
    {
        // Populate waypoints from children automatically
        if (waypointsParent != null)
        {
            waypoints.Clear();
            foreach (Transform child in waypointsParent)
            {
                waypoints.Add(child);
            }
        }
    }

    void Update()
    {
        if (!isMoving || waypoints.Count == 0)
            return;

        Transform target = waypoints[currentWaypoint];
        Vector3 targetPosition = target.position;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Rotate toward target
        if (moveDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                rotateSpeed * Time.deltaTime
            );
        }

        // Move
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        // Footsteps (only while moving)
        HandleFootsteps();

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
                    currentWaypoint = 0;
                }
                else
                {
                    animator.SetBool("IsMoving", false);
                    isMoving = false;
                    footstepTimer = 0f;
                }
            }
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
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
