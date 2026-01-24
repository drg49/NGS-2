using UnityEngine;

public class ChestInteract : Interactable
{
    [SerializeField] private GameObject chest;
    [SerializeField] private AudioSource chestOpenAudio;

    public override void Interact()
    {
        Animation anim = chest.GetComponent<Animation>();
        anim.Play("ChestAnim");
        chestOpenAudio.Play();
        anim.PlayQueued("AxeAnim");
        anim.PlayQueued("SleepingBagAnim");
        anim.PlayQueued("FlaskAnim");
        anim.PlayQueued("MatchesAnim");
        Destroy(gameObject);
    }
}
