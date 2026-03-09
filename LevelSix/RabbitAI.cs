using UnityEngine;

public class RabbitAI : MonoBehaviour
{
    [Header("Animation")]
    public Animator rabbitAnim;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float wanderRadius = 6f;
    public float idleTimeMin = 2f;
    public float idleTimeMax = 5f;

    [Header("Blood Effects (Pre-placed)")]
    public GameObject[] bloodSplatters; // assign the 7 inactive blood splatters here

    private Vector3 targetPosition;
    private bool isDead = false;
    private bool isMoving = false;
    private float idleTimer;

    void Start()
    {
        SetIdle();
        SetRandomIdleTimer();
    }

    void Update()
    {
        if (isDead)
            return;

        if (isMoving)
            MoveToTarget();
        else
        {
            idleTimer -= Time.deltaTime;

            if (idleTimer <= 0)
                PickNewTarget();
        }
    }

    void PickNewTarget()
    {
        Vector3 offset = new Vector3(
            Random.Range(-wanderRadius, wanderRadius),
            0,
            Random.Range(-wanderRadius, wanderRadius)
        );

        targetPosition = transform.position + offset;

        isMoving = true;

        rabbitAnim.SetInteger("AnimIndex", 1);
        rabbitAnim.SetTrigger("Next");
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        transform.LookAt(targetPosition);

        if (Vector3.Distance(transform.position, targetPosition) < 0.3f)
        {
            SetIdle();
            SetRandomIdleTimer();
        }
    }

    void SetIdle()
    {
        isMoving = false;

        rabbitAnim.SetInteger("AnimIndex", 0);
        rabbitAnim.SetTrigger("Next");
    }

    void SetRandomIdleTimer()
    {
        idleTimer = Random.Range(idleTimeMin, idleTimeMax);
    }

    public void Kill()
    {
        if (isDead)
            return;

        isDead = true;

        rabbitAnim.SetInteger("AnimIndex", 2);
        rabbitAnim.SetTrigger("Next");

        GetComponent<Collider>().enabled = false;

        // Blood will now be activated via animation event
    }

    // Call this function from an Animation Event at the end of the death animation
    public void ActivateRandomBlood()
    {
        if (bloodSplatters == null || bloodSplatters.Length == 0)
            return;

        // Disable all first, just in case
        foreach (var splatter in bloodSplatters)
            splatter.SetActive(false);

        // Activate a random splatter
        int index = Random.Range(0, bloodSplatters.Length);
        bloodSplatters[index].SetActive(true);
    }
}
