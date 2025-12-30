using UnityEngine;

public class CoffeePotPour : MonoBehaviour
{
    [SerializeField] private ParticleSystem pourParticles;
    [SerializeField] private GameObject coffeeInCup;
    [SerializeField] private AudioSource pourSound;

    // These methods will be called by Animation Events
    public void PlayPourParticles()
    {
        pourSound.Play();
        pourParticles.Play();
    }

    public void StopPourParticles()
    {
        pourParticles.Stop();
    }

    public void ShowCoffeeInCup()
    {
        coffeeInCup.SetActive(true);
        Destroy(pourParticles);
    }
}
