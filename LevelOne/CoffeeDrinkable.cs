using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoffeeDrinkable : ProgressFill
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI instructionalText;

    [Header("Audio")]
    [SerializeField] private AudioClip sipClip;

    [Header("Sip Times (seconds)")]
    [SerializeField] private float[] sipTimes = new float[] { 0.1f, 3.5f, 6.6f };

    // Next Event
    [SerializeField] private GameObject needAShower;

    private AudioSource audioSource;
    private bool[] sipPlayed;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        sipPlayed = new bool[sipTimes.Length];
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // Reset all sip flags
        for (int i = 0; i < sipPlayed.Length; i++)
            sipPlayed[i] = false;
    }

    protected override void Update()
    {
        base.Update();

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

    private void PlaySip()
    {
        audioSource.Stop();
        audioSource.clip = sipClip;
        audioSource.Play();
    }

    protected override void Complete()
    {
        base.Complete();

        instructionalText.text = "";
        // Reset flags just in case
        for (int i = 0; i < sipPlayed.Length; i++)
            sipPlayed[i] = false;

        // Trigger next event
        needAShower.SetActive(true);

        gameObject.SetActive(false);
    }
}
