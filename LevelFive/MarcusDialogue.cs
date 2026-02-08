using UnityEngine;

public class MarcusDialogue : NPCDialogue
{
    [SerializeField] private SphereCollider firepitCollider;
    [SerializeField] private GameObject firepitInteract;
    [SerializeField] private GameObject logObjective;
    [SerializeField] private GameObject logManager;
    [SerializeField] private GameObject startFireObjective;
    [SerializeField] private GameObject dropWoodAudio;

    private void OnEnable()
    {
        dialogueManager.OnDialogueFinished += DialogueEnded;
    }

    private void OnDisable()
    {
        dialogueManager.OnDialogueFinished -= DialogueEnded;
    }

    public override void Interact()
    {
        base.Interact();
        // Clean up
        Destroy(firepitInteract);
        Destroy(logObjective);
        Destroy(logManager);
        Destroy(dropWoodAudio);
    }

    private void DialogueEnded()
    {
        Destroy(cameraToActivate.gameObject);
        player.SetActive(true);
        firepitCollider.enabled = true;
        startFireObjective.SetActive(true);
        Destroy(gameObject);
    }
}
