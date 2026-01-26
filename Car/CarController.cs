using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float motorForce = 30f;    // acceleration force
    public float turnSpeed = 150f;    // degrees per second
    public float maxSpeed = 20f;      // max forward/backward speed

    private Rigidbody rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 1000f;
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        rb.linearDamping = 0.5f;
        rb.angularDamping = 2f;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float forward = moveInput.y;
        float turn = moveInput.x;

        // Limit speed
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        if (flatVelocity.magnitude < maxSpeed)
        {
            // Apply forward/backward acceleration
            rb.AddForce(transform.forward * forward * motorForce, ForceMode.Acceleration);
        }

        // Rotate car only when moving forward/backward
        if (Mathf.Abs(forward) > 0.05f)
        {
            float rotation = turn * turnSpeed * Time.fixedDeltaTime * Mathf.Sign(forward);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotation, 0f));
        }
    }
}
