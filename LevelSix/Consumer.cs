using UnityEngine;

public class Consumer : Interactable
{
    [Header("Consumption Settings")]
    [SerializeField] private float interval = 1f;
    [SerializeField] private AudioSource eatSound;

    [Header("Other Consumers & Colliders")]
    [SerializeField] private Consumer[] otherConsumers;
    [SerializeField] private Collider[] otherColliders;

    private GameObject[] portions;
    private int currentIndex;
    private float lastChange;
    private bool consuming;

    private BoxCollider myCollider;

    // Tracks whether this object has been completely consumed
    public bool IsConsumed { get; private set; } = false;

    public bool IsConsuming => consuming;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        bool skipFirst = transform.childCount > 4;
        int length = skipFirst ? transform.childCount - 1 : transform.childCount;

        portions = new GameObject[length];

        for (int i = 0; i < length; i++)
        {
            portions[i] = transform.GetChild(skipFirst ? i + 1 : i).gameObject;
            if (portions[i].activeInHierarchy)
                currentIndex = i;
        }
    }

    private void Update()
    {
        if (!consuming) return;

        if (Time.time - lastChange > interval)
        {
            ConsumePortion();
            lastChange = Time.time;
        }
    }

    public override void Interact()
    {
        if (consuming || IsConsumed) return;

        consuming = true;

        // Disable own collider
        if (myCollider != null)
            myCollider.enabled = false;

        // Disable other colliders while consuming
        SetOtherColliders(false);
    }

    private void ConsumePortion()
    {
        if (currentIndex >= portions.Length)
        {
            consuming = false;
            IsConsumed = true; // mark this as fully eaten

            // Only re-enable other colliders that are not consumed
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

    private void SetOtherColliders(bool state)
    {
        if (otherColliders == null) return;

        for (int i = 0; i < otherColliders.Length; i++)
        {
            Collider col = otherColliders[i];
            if (col == null) continue;

            if (state) // Only re-enable if the corresponding consumer is not consuming AND not already consumed
            {
                if (otherConsumers != null && i < otherConsumers.Length)
                {
                    Consumer other = otherConsumers[i];
                    if (other != null && (other.IsConsuming || other.IsConsumed))
                        continue; // skip enabling
                }
            }

            col.enabled = state;
        }
    }
}