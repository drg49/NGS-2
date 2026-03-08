using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FPSGunShoot : MonoBehaviour
{
    public Camera cam;
    public float shootDistance = 200f;
    public LayerMask rabbitLayer;

    public Image reticle;

    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack.performed += Shoot;
    }

    void OnDisable()
    {
        inputActions.Player.Attack.performed -= Shoot;
        inputActions.Player.Disable();
    }

    void Update()
    {
        UpdateReticleColor();
    }

    void UpdateReticleColor()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        // Check if the ray hits a rabbit
        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance, rabbitLayer))
        {
            if (hit.collider.TryGetComponent<RabbitAI>(out _))
            {
                reticle.color = Color.red;
                return;
            }
        }

        // Reset color if not aiming at a rabbit
        reticle.color = Color.white;
    }

    void Shoot(InputAction.CallbackContext ctx)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance, rabbitLayer))
        {
            
            if (hit.collider.TryGetComponent<RabbitAI>(out var rabbit))
                rabbit.Kill();
        }
    }
}
