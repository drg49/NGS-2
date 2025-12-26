using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runSpeed = 9f;
    public float gravity = -9.81f;

    [Header("Look Settings")]
    public float lookSensitivity = 1f;
    public bool invertY = false;
    public Transform playerCamera;
    public float cameraClamp = 90f;

    [Header("Footstep Settings")]
    public AudioSource footstepAudio;
    public AudioClip[] walkFootstepClips;
    public float walkStepRate = 0.5f;
    public float runStepRate = 0.35f;

    private CharacterController controller;
    private PlayerInputActions inputActions;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isSprinting;

    private float yaw;
    private float pitch;
    private float stepTimer;
    private float yVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        inputActions = new PlayerInputActions();

        // Movement
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;

        // Look
        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += _ => lookInput = Vector2.zero;

        // Sprint
        inputActions.Player.Sprint.performed += _ => isSprinting = true;
        inputActions.Player.Sprint.canceled += _ => isSprinting = false;
    }

    private void OnEnable() => inputActions.Player.Enable();
    private void OnDisable() => inputActions.Player.Disable();

    private void Start()
    {
        if (!playerCamera)
            playerCamera = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
        HandleFootsteps();
    }

    private void HandleMovement()
    {
        float speed = isSprinting ? runSpeed : moveSpeed;

        Vector3 direction = transform.forward * moveInput.y + transform.right * moveInput.x;

        if (controller.isGrounded && yVelocity < 0f)
            yVelocity = -2f;

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = direction.normalized * speed;
        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleLook()
    {
        float lookX = lookInput.x * lookSensitivity;
        float lookY = lookInput.y * lookSensitivity * (invertY ? 1f : -1f);

        yaw += lookX;
        pitch += lookY;
        pitch = Mathf.Clamp(pitch, -cameraClamp, cameraClamp);

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        playerCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void HandleFootsteps()
    {
        if (!controller.isGrounded || moveInput.magnitude < 0.1f)
        {
            stepTimer = 0f;
            return;
        }

        float stepRate = isSprinting ? runStepRate : walkStepRate;

        if (stepTimer <= 0f)
        {
            PlayFootstep(isSprinting);
            stepTimer = stepRate;
        }

        stepTimer -= Time.deltaTime;
    }

    private void PlayFootstep(bool running)
    {
        if (!footstepAudio || walkFootstepClips.Length == 0) return;

        AudioClip clip = walkFootstepClips[Random.Range(0, walkFootstepClips.Length)];

        footstepAudio.pitch = running
            ? Random.Range(1.0f, 1.15f)
            : Random.Range(0.9f, 1.05f);

        footstepAudio.PlayOneShot(clip);
    }

    // Public method to enable/disable player during pause
    public void SetPaused(bool paused)
    {
        enabled = !paused;
    }
}
