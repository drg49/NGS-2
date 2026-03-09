using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class RabbitAI : MonoBehaviour
{
    [Header("Animation")]
    public Animator rabbitAnim;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float wanderRadius = 6f;
    public float idleTimeMin = 2f;
    public float idleTimeMax = 5f;

    [Header("Blood Effects")]
    public GameObject[] bloodSplatters;

    [Header("Interaction")]
    public GameObject interactionSphere;
    public TextMeshPro interactionTextWorld; // assign in rabbit prefab (child)
    public float interactionDistance = 3f;

    [HideInInspector]
    public InputActionReference interactAction;

    private Vector3 targetPosition;
    private bool isDead = false;
    private bool isMoving = false;
    private float idleTimer;

    void Start()
    {
        SetIdle();
        SetRandomIdleTimer();

        if (interactionSphere != null)
            interactionSphere.SetActive(false);

        if (interactionTextWorld != null)
        {
            interactionTextWorld.gameObject.SetActive(false);
            SetupTextAlwaysVisible(interactionTextWorld);
        }
    }

    void Update()
    {
        if (isDead)
        {
            HandleRaycastInteraction();
            FaceCamera();
            return;
        }

        if (isMoving)
            MoveToTarget();
        else
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
                PickNewTarget();
        }

        FaceCamera();
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
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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
    }

    public void ActivateRandomBlood()
    {
        if (bloodSplatters == null || bloodSplatters.Length == 0)
            return;

        foreach (var splatter in bloodSplatters)
            splatter.SetActive(false);

        int index = Random.Range(0, bloodSplatters.Length);
        bloodSplatters[index].SetActive(true);

        if (interactionSphere != null)
            interactionSphere.SetActive(true);
    }

    private void HandleRaycastInteraction()
    {
        if (interactionTextWorld == null || interactionSphere == null || interactAction == null || interactAction.action == null)
            return;

        Camera cam = Camera.main;
        if (cam == null)
            return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject == interactionSphere)
            {
                if (!interactionTextWorld.gameObject.activeSelf)
                    interactionTextWorld.gameObject.SetActive(true);

                string buttonName = interactAction.action.bindings[0].ToDisplayString();
                interactionTextWorld.text = $"Grab [{buttonName}]";

                if (interactAction.action.WasPressedThisFrame())
                    TakeRabbit();

                return;
            }
        }

        // Not hovering over rabbit
        if (interactionTextWorld.gameObject.activeSelf)
            interactionTextWorld.gameObject.SetActive(false);
    }

    private void TakeRabbit()
    {
        if (interactionTextWorld != null)
            interactionTextWorld.gameObject.SetActive(false);

        Destroy(gameObject);
    }

    private void FaceCamera()
    {
        if (interactionTextWorld == null || Camera.main == null)
            return;

        interactionTextWorld.transform.rotation = Quaternion.LookRotation(interactionTextWorld.transform.position - Camera.main.transform.position);
    }

    private void SetupTextAlwaysVisible(TextMeshPro text)
    {
        var renderer = text.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;
            renderer.material.renderQueue = 5000;
        }
    }
}