using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float motorForce = 30f;
    public float turnSpeed = 150f;
    public float maxSpeed = 20f;

    [Header("Audio")]
    public AudioSource engineAudio;   // assign in Inspector

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

        // Start engine idle
        if (engineAudio != null)
        {
            engineAudio.loop = true;
            engineAudio.playOnAwake = false;
            engineAudio.Play();
        }
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

        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * forward * motorForce, ForceMode.Acceleration);
        }

        if (Mathf.Abs(forward) > 0.05f)
        {
            float rotation = turn * turnSpeed * Time.fixedDeltaTime * Mathf.Sign(forward);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotation, 0f));
        }
    }
}
