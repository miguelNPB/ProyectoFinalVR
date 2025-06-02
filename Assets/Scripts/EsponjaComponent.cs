using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsponjaComponent : MonoBehaviour
{
    public float wetTime;
    public bool wet;
    [SerializeField] private Material drySponge;
    [SerializeField] private Material wetSponge;
    [SerializeField] GameObject wetParticles;
    [SerializeField] GameObject cleaningParticles;

    private bool calledDry;
    private Renderer renderer;
    private AudioSource audioSource;

    public void ActivateCleaningParticles()
    {
        cleaningParticles.SetActive(true);
    }
    public void DeactivateCleaningParticles()
    {
        cleaningParticles.SetActive(false);
    }
    private void WetSponge()
    {
        if (!wet)
        {
            wet = true;
            wetParticles.SetActive(true);
            renderer.material = wetSponge;
            audioSource.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.SpongeWet);
            audioSource.Play();
        }
    }
    private void DrySponge()
    {
        wet = false;
        calledDry = false;
        wetParticles.SetActive(false);
        cleaningParticles.SetActive(false);
        renderer.material = drySponge;
        audioSource.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.SpongeDry);
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            WetSponge();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            if (!calledDry)
            {
                calledDry = true;
                Invoke("DrySponge", wetTime);
            }
        }
    }

    private void Start()
    {
        calledDry = false;
        wet = false;

        renderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        wetParticles.SetActive(false);
        cleaningParticles.SetActive(false);
    }
}
