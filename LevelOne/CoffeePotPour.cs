using UnityEngine;

public class CoffeePotPour : MonoBehaviour
{
    [SerializeField] private ParticleSystem pourParticles;
    [SerializeField] private GameObject coffeeInCup;
    [SerializeField] private AudioSource pourSound;
    [SerializeField] private CoffeeCupInteract coffeeCupInteract;

    // Animation Event
    public void PlayPourParticles()
    {
        pourSound.Play();
        pourParticles.Play();
    }

    // Animation Event
    public void StopPourParticles()
    {
        pourParticles.Stop();
    }

    // Animation Event (LAST FRAME)
    public void ShowCoffeeInCup()
    {
        coffeeInCup.SetActive(true);
        Destroy(pourParticles);

        // Notify cup that pouring is done
        coffeeCupInteract.OnPourFinished();
    }
}
