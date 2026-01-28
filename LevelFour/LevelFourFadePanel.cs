using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFourFadePanel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI fadeToLvlFiveTxt;

    public void StartLevelFiveTransition()
    {
        // Start the coroutine
        StartCoroutine(FadeTextSequence());
    }

    private readonly float fadeDuration = 1f;
    private readonly float displayTime = 6f;

    private IEnumerator FadeTextSequence()
    {
        // Step 1: Wait before starting fade
        yield return new WaitForSeconds(1f);

        // Step 2: Ensure text is active and fully transparent
        fadeToLvlFiveTxt.gameObject.SetActive(true);
        Color c = fadeToLvlFiveTxt.color;
        fadeToLvlFiveTxt.color = new Color(c.r, c.g, c.b, 0f);

        // Step 3: Fade in
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            fadeToLvlFiveTxt.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeToLvlFiveTxt.color = new Color(c.r, c.g, c.b, 1f);

        // Step 4: Keep text fully visible
        yield return new WaitForSeconds(displayTime);

        // Step 5: Fade out
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsed / fadeDuration));
            fadeToLvlFiveTxt.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        fadeToLvlFiveTxt.color = new Color(c.r, c.g, c.b, 0f);
        fadeToLvlFiveTxt.gameObject.SetActive(false);

        // Step 6: Call level transition
        GoToLevelFive();
    }

    private void GoToLevelFive()
    {
        SceneManager.LoadScene("FifthLevel_Campsite");
    }
}
