using TMPro;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InstructionTextAlphaFader : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float visibleDuration = 5f;
    [SerializeField] private float fadeOutDuration = 0.5f;

    private TextMeshProUGUI instructionText;
    private Coroutine fadeRoutine;

    private void Awake()
    {
        instructionText = GetComponent<TextMeshProUGUI>();
        SetAlpha(0f);
    }

    public void Show(string message)
    {
        instructionText.text = message;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        yield return Fade(0f, 1f, fadeInDuration);
        yield return new WaitForSeconds(visibleDuration);
        yield return Fade(1f, 0f, fadeOutDuration);
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
}
