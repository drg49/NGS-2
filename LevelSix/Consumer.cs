using UnityEngine;

public class Consumer : MonoBehaviour
{
    GameObject[] portions;
    int currentIndex;

    float lastChange;
    readonly float interval = 1f;

    [SerializeField] private AudioSource eatSound;

    void Start()
    {
        bool skipFirst = transform.childCount > 4;

        portions = new GameObject[
            skipFirst ? transform.childCount - 1 : transform.childCount
        ];

        for (int i = 0; i < portions.Length; i++)
        {
            portions[i] = transform.GetChild(skipFirst ? i + 1 : i).gameObject;

            if (portions[i].activeInHierarchy)
                currentIndex = i;
        }
    }

    void Update()
    {
        if (Time.time - lastChange > interval)
        {
            Consume();
            lastChange = Time.time;
        }
    }

    public void Consume()
    {
        // Stop if all portions are eaten
        if (currentIndex >= portions.Length)
            return;

        // Play eat sound
        if (eatSound != null)
            eatSound.Play();

        // Disable current portion
        portions[currentIndex].SetActive(false);

        currentIndex++;

        // Enable next portion if it exists
        if (currentIndex < portions.Length)
            portions[currentIndex].SetActive(true);
    }
}