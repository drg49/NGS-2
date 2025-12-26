using UnityEngine;
using UnityEngine.InputSystem;

public class BedCameraLook : MonoBehaviour
{
    [Header("Look Settings")]
    public float sensitivity = 7.5f;      // How fast the camera rotates
    public Transform playerBody;          // Parent object (BedPlayer)
    public float minX = -30f;             // Look down limit
    public float maxX = 8f;              // Look up limit
    public float minY = -45f;             // Look left limit
    public float maxY = 30f;              // Look right limit

    [Header("Input")]
    public InputActionReference lookAction; // Your existing Look action

    private float xRotation;
    private float yRotation;
    private Vector2 lookInput;

    private Quaternion initialCameraRotation;
    private Quaternion initialBodyRotation;

    void OnEnable()
    {
        lookAction.action.Enable();
    }

    void OnDisable()
    {
        lookAction.action.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Store initial rotations
        initialCameraRotation = transform.localRotation;
        initialBodyRotation = playerBody.localRotation;

        // Initialize x/y rotation based on current rotation
        Vector3 camEuler = transform.localEulerAngles;
        xRotation = camEuler.x > 180f ? camEuler.x - 360f : camEuler.x;

        Vector3 bodyEuler = playerBody.localEulerAngles;
        yRotation = bodyEuler.y > 180f ? bodyEuler.y - 360f : bodyEuler.y;
    }

    void Update()
    {
        lookInput = lookAction.action.ReadValue<Vector2>();

        float mouseX = lookInput.x * sensitivity * Time.deltaTime;
        float mouseY = lookInput.y * sensitivity * Time.deltaTime;

        // Adjust pitch and yaw with clamping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minX, maxX);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, minY, maxY);

        // Apply rotations relative to initial rotations
        transform.localRotation = initialCameraRotation * Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.localRotation = initialBodyRotation * Quaternion.Euler(0f, yRotation, 0f);
    }
}
