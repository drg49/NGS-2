using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WineDrinkable : ProgressFill
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI instructionalText;

    [Header("Audio")]
    [SerializeField] private AudioClip sipClip;

    [Header("Sip Times (seconds)")]
    [SerializeField] private float[] sipTimes = new float[] { 0.1f, 3.5f, 6.6f };

    [Header("Bottle Tilt")]
    [SerializeField] private float tiltAngle = 45f;   // how far back
    [SerializeField] private float tiltSpeed = 6f;    // smoothness

    private AudioSource audioSource;
    private bool[] sipPlayed;

    private Quaternion startRotation;

    // -------------------------------------------------
    // Awake
    // -------------------------------------------------
    protected override void Awake()
    {
        base.Awake();

        startRotation = transform.localRotation;

        string button = inputActions.Player.Interact.bindings[0].ToDisplayString();
        instructionalText.text = $"Hold [{button}] to drink";

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        sipPlayed = new bool[sipTimes.Length];
    }

    // -------------------------------------------------
    // Enable
    // -------------------------------------------------
    protected override void OnEnable()
    {
        base.OnEnable();

        for (int i = 0; i < sipPlayed.Length; i++)
            sipPlayed[i] = false;
    }

    // -------------------------------------------------
    // Update
    // -------------------------------------------------
    protected override void Update()
    {
        base.Update();

        HandleBottleTilt();

        if (!isHolding || isComplete || sipClip == null)
            return;

        for (int i = 0; i < sipTimes.Length; i++)
        {
            if (!sipPlayed[i] && progress >= sipTimes[i])
            {
                PlaySip();
                sipPlayed[i] = true;
            }
        }
    }

    // -------------------------------------------------
    // Tilt logic
    // -------------------------------------------------
    private void HandleBottleTilt()
    {
        Quaternion targetRotation =
            (isHolding && !isComplete)
                ? startRotation * Quaternion.Euler(-tiltAngle, 0f, 0f)
                : startRotation;

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * tiltSpeed
        );
    }

    // -------------------------------------------------
    // Audio
    // -------------------------------------------------
    private void PlaySip()
    {
        audioSource.Stop();
        audioSource.clip = sipClip;
        audioSource.Play();
    }

    // -------------------------------------------------
    // Complete
    // -------------------------------------------------
    protected override void Complete()
    {
        base.Complete();

        instructionalText.text = "";

        for (int i = 0; i < sipPlayed.Length; i++)
            sipPlayed[i] = false;

        Debug.Log("Done drinking");

        Destroy(gameObject);
    }
}
