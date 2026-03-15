using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Consumer : Interactable
{
    [Header("Consumption Settings")]
    [SerializeField] private float interval = 1f;
    [SerializeField] private AudioSource eatSound;

    [Header("Other Consumers & Colliders")]
    [SerializeField] private Consumer[] otherConsumers;
    [SerializeField] private Collider[] otherColliders;

    [SerializeField] private List<GameObject> davidWalkOutsideWaypoints;
    [SerializeField] private PathWalker davidPW;
    [SerializeField] private Animator davidAnim;

    private GameObject[] portions;
    private int currentIndex;
    private float lastChange;
    private bool consuming;

    private BoxCollider myCollider;

    public bool IsConsumed { get; private set; } = false;
    public bool IsConsuming => consuming;

    // Static list to track all Consumer instances
    private static List<Consumer> allConsumers = new List<Consumer>();

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider>();
        allConsumers.Add(this); // register this instance
    }

    private void OnDestroy()
    {
        allConsumers.Remove(this); // unregister on destroy
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

        if (myCollider != null)
            myCollider.enabled = false;

        SetOtherColliders(false);
    }

    private void ConsumePortion()
    {
        if (currentIndex >= portions.Length)
        {
            consuming = false;
            IsConsumed = true;

            SetOtherColliders(true);

            // Debug log when all game objects with Consumer scripts have been interacted with
            if (AllConsumersConsumed())
            {
                StartCoroutine(WaitAndMoveDavid());
            }

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

    // Static method to check if all Consumers have been fully consumed
    public static bool AllConsumersConsumed()
    {
        return allConsumers.All(c => c != null && c.IsConsumed);
    }

    private IEnumerator WaitAndMoveDavid()
    {
        yield return new WaitForSeconds(8f);

        // David begins to walk outside from the kitchen
        foreach (GameObject obj in davidWalkOutsideWaypoints)
        {
            obj.SetActive(true);
        }

        // David's pathwalker should already be disabled at this point
        // So to refresh it we just need to turn it back on
        davidPW.enabled = true;
        davidAnim.SetTrigger("WalkOutside");
    }
}
