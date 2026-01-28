using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float motorForce = 30f;
    public float turnSpeed = 150f;
    public float maxSpeed = 20f;

    [Header("Steering Wheel")]
    public Transform steeringWheel; // assign PIVOT, not mesh
    public float maxSteerWheelAngle = 60f;
    public float steeringWheelSmooth = 8f;

    [Header("Audio")]
    public AudioSource engineIdle;
    public AudioSource engineAccelerate;
    public float idleVolume = 0.01f;
    public float accelVolume = 0.4f;
    public float fadeTime = 0.4f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private Coroutine accelFadeRoutine;
    private bool accelerating;

    private float currentSteerAngle;

    // -------------------- LIFECYCLE --------------------

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

    void OnEnable()
    {
        // Idle engine (constant)
        if (engineIdle != null)
        {
            engineIdle.loop = true;
            engineIdle.playOnAwake = false;
            engineIdle.volume = idleVolume;
            engineIdle.Play();
        }

        // Acceleration engine (fades in/out)
        if (engineAccelerate != null)
        {
            engineAccelerate.loop = true;
            engineAccelerate.playOnAwake = false;
            engineAccelerate.volume = 0f;
            engineAccelerate.Play();
        }
    }

    void OnDisable()
    {
        if (engineIdle != null)
            engineIdle.Stop();

        if (engineAccelerate != null)
            engineAccelerate.Stop();

        accelerating = false;
    }

    // -------------------- INPUT --------------------

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // -------------------- UPDATE --------------------

    void FixedUpdate()
    {
        HandleMovement();
        HandleEngineAudio();
        HandleSteeringWheel();
    }

    // -------------------- MOVEMENT --------------------

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

    // -------------------- STEERING WHEEL --------------------

    void HandleSteeringWheel()
    {
        if (steeringWheel == null) return;

        float targetAngle = -moveInput.x * maxSteerWheelAngle;

        currentSteerAngle = Mathf.Lerp(
            currentSteerAngle,
            targetAngle,
            Time.fixedDeltaTime * steeringWheelSmooth
        );

        steeringWheel.localRotation = Quaternion.Euler(0f, 0f, currentSteerAngle);
    }

    // -------------------- ENGINE AUDIO --------------------

    void HandleEngineAudio()
    {
        bool wantsAcceleration = Mathf.Abs(moveInput.y) > 0.05f;

        if (wantsAcceleration != accelerating)
        {
            accelerating = wantsAcceleration;
            FadeAccelTo(accelerating ? accelVolume : 0f);
        }
    }

    void FadeAccelTo(float targetVolume)
    {
        if (engineAccelerate == null) return;

        if (accelFadeRoutine != null)
            StopCoroutine(accelFadeRoutine);

        accelFadeRoutine = StartCoroutine(FadeAudio(engineAccelerate, targetVolume));
    }

    IEnumerator FadeAudio(AudioSource source, float targetVolume)
    {
        float start = source.volume;
        float t = 0f;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(start, targetVolume, t / fadeTime);
            yield return null;
        }

        source.volume = targetVolume;
    }
}
