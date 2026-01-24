using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOneManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bedPlayer;
    [SerializeField] private TextMeshProUGUI interactionText;

    [Header("Pause Reference")]
    [SerializeField] private PauseMenuController pauseMenu;
    public RenderTexture renderTexture;

    [Header("LevelOne")]
    [SerializeField] private List<GameObject> objectsToDestroyOnLevelOne;

    [Header("LevelThree")]
    [SerializeField] private List<GameObject> objectsToDestroyOnLevelThree;
    [SerializeField] private Transform levelThreeSpawn;

    // one-time interaction guard to determine if player has already left the bed
    private bool hasInteracted = false;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = activeRT;
    }

    private void Start()
    {
        // Scene config happens ON LOAD
        // Level 1 shares a scene with Level 3
        switch (SceneContext.CurrentLevelMode)
        {
            case LevelMode.LevelThree:
                SetupLevelThree();
                break;

            case LevelMode.LevelOne:
            default:
                SetupLevelOne();
                break;
        }
    }

    private void SetupLevelOne()
    {
        Debug.Log("Level 1");
        foreach (GameObject obj in objectsToDestroyOnLevelOne)
        {
            Destroy(obj);
        }
    }

    private void SetupLevelThree()
    {
        Debug.Log("Level 3");
        bedPlayer.SetActive(false);
        player.SetActive(true);
        // Level Three Spawn Point
        player.transform.SetPositionAndRotation(
            levelThreeSpawn.position,
            levelThreeSpawn.rotation * Quaternion.Euler(0, 90f, 0)
        );

        Debug.Log(player.transform);

        // Destroy Level One Events & Objects
        foreach (GameObject obj in objectsToDestroyOnLevelThree)
        {
            Destroy(obj);
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        // Player has already left the bed
        if (hasInteracted)
            return;

        // paused
        if (pauseMenu != null && pauseMenu.IsPaused)
            return;

        // mark as consumed
        hasInteracted = true;

        // CRITICAL: destroy first (active camera)
        Destroy(bedPlayer);
        player.SetActive(true);

        if (interactionText != null)
            interactionText.text = "";
    }
}
