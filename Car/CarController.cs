using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float motorForce = 50f;    // forward/backward force (Acceleration mode ignores mass)
    public float turnSpeed = 150f;    // degrees per second

    private Rigidbody rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 1000f;
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // lower center of mass for stability
        rb.linearDamping = 0.5f;   // slows sliding
        rb.angularDamping = 2f;    // prevents wild spinning
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // Called automatically by PlayerInput
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

        // Apply forward/backward force (ignores mass)
        rb.AddForce(transform.forward * forward * motorForce * Time.fixedDeltaTime, ForceMode.Acceleration);

        // Rotate car only if moving forward/backward
        if (Mathf.Abs(forward) > 0.05f)
        {
            float rotation = turn * turnSpeed * Time.fixedDeltaTime * Mathf.Sign(forward);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotation, 0f));
        }
    }
}
