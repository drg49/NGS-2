using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletInteract : Interactable
{
    [Header("Objectives")]
    [SerializeField] private PauseMenuObjectivesController objectivesController;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private GameObject toiletPlayer;
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject jumpscare;
    [SerializeField] private AudioSource sound;
    [SerializeField] private GameObject particles;
    [SerializeField] protected IndoorAmbienceZone indoorAmbience;

    public override void Interact()
    {
        indoorAmbience.DisablePlayerIndoors();
        toiletPlayer.SetActive(true);

        // Activate jumpscare early
        jumpscare.SetActive(true);

        sound.Play();
        StartCoroutine(DestroyParticlesWhenSoundEnds());

        if (objectivesController != null)
        {
            objectivesController.SetObjectives(
                new List<string>
                {
                    "Meet Marcus & David at Big Burger",
                    "Order food at the register",
                    "Sit down with Marcus & David",
                    "Use the restroom",
                    ""
                },
                4
            );
        }
    }

    private IEnumerator DestroyParticlesWhenSoundEnds()
    {
        // Wait until the sound finishes playing
        yield return new WaitWhile(() => sound.isPlaying);

        // Destroy particle GameObject
        var ps = particles.GetComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        Destroy(particles, ps.main.startLifetime.constantMax);
        fadeAnimator.SetTrigger("FadeLeaveToilet");
        Destroy(gameObject);
    }
}
