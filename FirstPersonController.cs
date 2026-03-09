using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float runSpeed = 5.5f;
    public float gravity = -9.81f;
    public bool canRun = true;

    [Header("Look Settings")]
    public float lookSensitivity = 0.1f;
    public bool invertY = false;
    public Transform playerCamera;
    public float cameraClamp = 90f;

    [Header("Footstep Settings")]
    public AudioSource footstepAudio;
    public float walkStepRate = 0.5f;
    public float runStepRate = 0.35f;

    [Header("Footstep Surfaces")]
    [SerializeField] private AudioClip[] defaultFootsteps;
    [SerializeField] private AudioClip[] secondaryFootsteps;
    private AudioClip[] currentFootsteps;

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

        lookSensitivity = PlayerPrefs.GetFloat("LookSensitivity", lookSensitivity);

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += _ => lookInput = Vector2.zero;

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

        yaw = transform.eulerAngles.y;
        pitch = playerCamera.localEulerAngles.x;

        if (pitch > 180f)
            pitch -= 360f;

        currentFootsteps = defaultFootsteps;
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
        HandleFootsteps();
    }

    private void HandleMovement()
    {
        float speed = (isSprinting && canRun) ? runSpeed : moveSpeed;

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

        bool running = isSprinting && canRun;
        float stepRate = running ? runStepRate : walkStepRate;

        if (stepTimer <= 0f)
        {
            PlayFootstep(running);
            stepTimer = stepRate;
        }

        stepTimer -= Time.deltaTime;
    }

    private void PlayFootstep(bool running)
    {
        if (!footstepAudio || currentFootsteps == null || currentFootsteps.Length == 0) return;

        AudioClip clip = currentFootsteps[Random.Range(0, currentFootsteps.Length)];

        footstepAudio.pitch = running
            ? Random.Range(1.0f, 1.15f)
            : Random.Range(0.9f, 1.05f);

        footstepAudio.PlayOneShot(clip);
    }

    public void SetFootsteps(bool useSecondary)
    {
        currentFootsteps = useSecondary ? secondaryFootsteps : defaultFootsteps;
    }

    public void SetPaused(bool paused)
    {
        enabled = !paused;
    }

    public void SetLookSensitivity(float newSensitivity)
    {
        lookSensitivity = newSensitivity;
        PlayerPrefs.SetFloat("LookSensitivity", newSensitivity);
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}