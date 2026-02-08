using TMPro;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class CampfireBurn : ProgressFill
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI instructionalText;

    [Header("Audio")]
    [SerializeField] private AudioClip burnClip;

    [Header("Burn Times (seconds)")]
    [SerializeField] private float[] burnTimes = new float[] { 0.1f, 3.5f, 6.6f };

    // =========================
    // Skewer Movement
    // =========================
    [Header("Skewer Movement")]
    [SerializeField] private Transform skewer;
    [SerializeField] private float forwardDistance = 0.4f;
    [SerializeField] private float moveSpeed = 6f;

    private Vector3 skewerStartLocalPos;

    // =========================

    private AudioSource audioSource;
    private bool[] burnPlayed;

    // =========================

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        burnPlayed = new bool[burnTimes.Length];

        if (skewer != null)
            skewerStartLocalPos = skewer.localPosition;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        for (int i = 0; i < burnPlayed.Length; i++)
            burnPlayed[i] = false;
    }

    // =========================
    // MAIN UPDATE
    // =========================

    protected override void Update()
    {
        base.Update();

        HandleSkewerMovement();

        if (!isHolding || isComplete || burnClip == null)
            return;

        for (int i = 0; i < burnTimes.Length; i++)
        {
            if (!burnPlayed[i] && progress >= burnTimes[i])
            {
                PlayBurn();
                burnPlayed[i] = true;
            }
        }
    }

    // =========================
    // Skewer movement logic
    // =========================

    private void HandleSkewerMovement()
    {
        if (skewer == null)
            return;

        Vector3 target = skewerStartLocalPos;

        // Only allow forward motion while cooking
        if (isHolding && !isComplete)
            target += Vector3.forward * forwardDistance;

        skewer.localPosition = Vector3.Lerp(
            skewer.localPosition,
            target,
            moveSpeed * Time.deltaTime
        );
    }

    // =========================

    private void PlayBurn()
    {
        audioSource.Stop();
        audioSource.clip = burnClip;
        audioSource.Play();
    }

    // =========================
    // COMPLETE
    // =========================

    protected override void Complete()
    {
        base.Complete();

        instructionalText.text = "";

        for (int i = 0; i < burnPlayed.Length; i++)
            burnPlayed[i] = false;

        Debug.Log("It's done cooking");

        StartCoroutine(ReturnAndDestroy());
    }

    private IEnumerator ReturnAndDestroy()
    {
        // Force no more holding input
        isHolding = false;

        float t = 0f;

        // Smoothly return to original position
        while (skewer != null &&
               Vector3.Distance(skewer.localPosition, skewerStartLocalPos) > 0.01f)
        {
            skewer.localPosition = Vector3.Lerp(
                skewer.localPosition,
                skewerStartLocalPos,
                moveSpeed * Time.deltaTime
            );

            t += Time.deltaTime;
            yield return null;
        }

        // Snap exactly
        if (skewer != null)
            skewer.localPosition = skewerStartLocalPos;

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
