using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FPSGunShoot : MonoBehaviour
{
    [Header("Camera & Shooting")]
    public Camera cam;
    public float shootDistance = 200f;
    public LayerMask rabbitLayer;

    [Header("Reticle UI")]
    public Image reticle;

    [Header("Effects")]
    public AudioSource rifleAudio;
    public ParticleSystem muzzleFlash;
    public float recoilAmount = 5f;
    public float recoilSpeed = 10f;

    private Vector3 originalPosition;
    private bool isRecoiling = false;
    private Vector3 recoilTarget;

    private PlayerInputActions inputActions;

    void Awake()
    {
        inputActions = new PlayerInputActions();
        originalPosition = transform.localPosition;
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
        UpdateRecoil();
    }

    private void UpdateRecoil()
    {
        if (isRecoiling)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, recoilTarget, recoilSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, recoilTarget) < 0.01f)
            {
                isRecoiling = false;
            }
        }
        else
        {
            // Smoothly return to original position
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, recoilSpeed * Time.deltaTime);
        }
    }

    void UpdateReticleColor()
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance, rabbitLayer))
        {
            if (hit.collider.TryGetComponent<RabbitAI>(out _))
            {
                reticle.color = Color.red;
                return;
            }
        }

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

        // Play effects
        PlayEffects();
    }

    private void PlayEffects()
    {
        rifleAudio.Play();
        muzzleFlash.Play();

        // Apply recoil
        recoilTarget = originalPosition - transform.forward * recoilAmount;
        isRecoiling = true;
    }
}