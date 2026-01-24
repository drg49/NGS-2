using TMPro;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InstructionTextAlphaFader : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float defaultVisibleDuration = 5f;
    [SerializeField] private float fadeOutDuration = 0.5f;

    private TextMeshProUGUI instructionText;
    private Coroutine fadeRoutine;

    private void Awake()
    {
        instructionText = GetComponent<TextMeshProUGUI>();
        SetAlpha(0f);
    }

    /// <summary>
    /// Shows instruction text.
    /// Pass -1 for infinite duration (manual hide).
    /// </summary>
    public void Show(string message, float overrideVisibleDuration = -999f)
    {
        instructionText.text = message;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        float durationToUse =
            overrideVisibleDuration >= -1f
                ? overrideVisibleDuration
                : defaultVisibleDuration;

        fadeRoutine = StartCoroutine(FadeSequence(durationToUse));
    }

    private IEnumerator FadeSequence(float visibleDuration)
    {
        // Fade in
        yield return Fade(0f, 1f, fadeInDuration);

        // Stay visible (if not infinite)
        if (visibleDuration >= 0f)
        {
            yield return new WaitForSeconds(visibleDuration);
            yield return Fade(1f, 0f, fadeOutDuration);
        }
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(from, to, t / duration));
            yield return null;
        }

        SetAlpha(to);
    }

    private void SetAlpha(float alpha)
    {
        Color c = instructionText.color;
        c.a = alpha;
        instructionText.color = c;
    }

    /// <summary>
    /// Optional manual hide (useful for infinite instructions).
    /// </summary>
    public void Hide()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(Fade(1f, 0f, fadeOutDuration));
    }
}
