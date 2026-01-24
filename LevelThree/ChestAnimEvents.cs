using UnityEngine;

public class ChestAnimEvents : MonoBehaviour
{
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject sleepingBag;
    [SerializeField] private GameObject flask;
    [SerializeField] private GameObject matches;
    [SerializeField] private AudioSource grabAudio;

    public void DestroyAxe()
    {
        grabAudio.Play();
        Destroy(axe);
    }

    public void DestroySleepingBag()
    {
        grabAudio.Play();
        Destroy(sleepingBag);
    }

    public void DestroyFlask()
    {
        grabAudio.Play();
        Destroy(flask);
    }

    public void DestroyMatches()
    {
        grabAudio.Play();
        Destroy(matches);
        GetComponent<InstructionSequence>().Play();
    }
}
