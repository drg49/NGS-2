using UnityEngine;

public class Consumer : Interactable
{
    GameObject[] portions;
    int currentIndex;

    float lastChange;
    [SerializeField] private float interval = 1f;

    bool consuming;

    [SerializeField] private AudioSource eatSound;

    // Colliders to disable while this one is consuming
    [SerializeField] private Collider[] otherColliders;

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
        if (!consuming)
            return;

        if (Time.time - lastChange > interval)
        {
            Consume();
            lastChange = Time.time;
        }
    }

    public override void Interact()
    {
        if (consuming)
            return;

        consuming = true;

        SetOtherColliders(false);
    }

    void Consume()
    {
        if (currentIndex >= portions.Length)
        {
            consuming = false;
            SetOtherColliders(true);
            return;
        }

        if (eatSound != null)
            eatSound.PlayOneShot(eatSound.clip);

        portions[currentIndex].SetActive(false);

        currentIndex++;

        if (currentIndex < portions.Length)
            portions[currentIndex].SetActive(true);
    }

    void SetOtherColliders(bool state)
    {
        if (otherColliders == null)
            return;

        foreach (var col in otherColliders)
        {
            if (col != null)
                col.enabled = state;
        }
    }
}