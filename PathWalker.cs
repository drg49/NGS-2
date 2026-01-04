using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PathWalker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    [Header("Path Settings")]
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private bool loopPath = false;
    [SerializeField] private float waypointReachDistance = 0.1f;

    [Header("Footsteps")]
    [SerializeField] private AudioClip[] footstepAudioClips;
    [SerializeField] private float footstepInterval = 0.5f;

    private Rigidbody rb;
    private int currentWaypoint = 0;
    private bool isMoving = true;
    private float footstepTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Required physics configuration
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate()
    {
        if (!isMoving || waypoints.Count == 0)
            return;

        // ?? Prevent player push impulses (CRITICAL)
        rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);

        Transform target = waypoints[currentWaypoint];

        // Horizontal-only movement vector
        Vector3 toTarget = target.position - rb.position;
        Vector3 flatDirection = new Vector3(toTarget.x, 0f, toTarget.z);

        // ===== Rotation (Y only) =====
        if (flatDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatDirection.normalized);
            rb.MoveRotation(
                Quaternion.Slerp(
                    rb.rotation,
                    targetRotation,
                    rotateSpeed * Time.fixedDeltaTime
                )
            );
        }

        // ===== Movement =====
        rb.MovePosition(
            rb.position + flatDirection.normalized * moveSpeed * Time.fixedDeltaTime
        );

        // ===== Animation =====
        animator.SetBool("IsMoving", true);

        // ===== Footsteps =====
        HandleFootsteps();

        // ===== Waypoint check (XZ plane ONLY) =====
        Vector3 flatCurrent = new Vector3(rb.position.x, 0f, rb.position.z);
        Vector3 flatTarget = new Vector3(target.position.x, 0f, target.position.z);

        if ((flatTarget - flatCurrent).sqrMagnitude <= waypointReachDistance * waypointReachDistance)
        {
            AdvanceWaypoint();
        }
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
            animator.SetBool("IsMoving", false);
            isMoving = false;
            footstepTimer = 0f;
        }
    }

    private void HandleFootsteps()
    {
        if (!isMoving || footstepAudioClips.Length == 0)
            return;

        footstepTimer -= Time.fixedDeltaTime;

        if (footstepTimer <= 0f)
        {
            audioSource.PlayOneShot(
                footstepAudioClips[Random.Range(0, footstepAudioClips.Length)]
            );
            footstepTimer = footstepInterval;
        }
    }
}
