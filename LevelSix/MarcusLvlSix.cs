using UnityEngine;

public class MarcusLvlSix : MonoBehaviour
{
    [SerializeField] private GameObject helpMarcusInteract;
    [SerializeField] private AudioSource bodyFall;
    [SerializeField] private AudioSource malePainfulGrunt;

    public void PlayBodyFall()
    {
        bodyFall.Play();
    }

    public void FallOver()
    {
        helpMarcusInteract.SetActive(true);
        malePainfulGrunt.Play();
    }
}
