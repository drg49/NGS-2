using UnityEngine;
using UnityEngine.InputSystem;

public class FPSGunShoot : MonoBehaviour
{
    public Camera cam;
    public float shootDistance = 200f;
    public LayerMask rabbitLayer;

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

    void Shoot(InputAction.CallbackContext ctx)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootDistance, rabbitLayer))
        {
            RabbitAI rabbit = hit.collider.GetComponent<RabbitAI>();

            if (rabbit != null)
                rabbit.Kill();
        }
    }
}