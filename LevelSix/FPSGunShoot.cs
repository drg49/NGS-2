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

    [Header("Recoil")]
    public float recoilAmount = 0.3f;
    public float recoilSpeed = 25f;

    [Header("Fire Rate")]
    public float fireCooldown = 0.6f;

    private float nextFireTime;

    private Vector3 originalPosition;
    private Vector3 recoilTarget;
    private bool isRecoiling;

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

    void UpdateRecoil()
    {
        Vector3 target = isRecoiling ? recoilTarget : originalPosition;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            target,
            recoilSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.localPosition, recoilTarget) < 0.001f)
        {
            isRecoiling = false;
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
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + fireCooldown;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance, rabbitLayer))
        {
            if (hit.collider.TryGetComponent<RabbitAI>(out var rabbit))
            {
                rabbit.Kill();
            }
        }

        PlayEffects();
    }

    void PlayEffects()
    {
        rifleAudio.Play();

        muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        muzzleFlash.Play();

        recoilTarget = originalPosition + new Vector3(0f, 0.01f, -recoilAmount);
        isRecoiling = true;
    }
}