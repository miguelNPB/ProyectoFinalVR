using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManchaParedComponent : MonoBehaviour
{
    public float progressIncrements = 0.001f;

    private float _progress;
    private SpriteRenderer _sprite;

    private float maxProgress;

    private EsponjaComponent esponjaComponent;
    private bool esponjaCleaning;

    private AudioSource audioSource;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _progress = 0;
        maxProgress = _sprite.color.a;

        esponjaCleaning = false;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.CleaningWall);
    }

    private void Update()
    {
        if (esponjaCleaning && esponjaComponent.wet)
            TickClean();
        else
            audioSource.Stop();
    }

    private void TickClean()
    {
        _progress += progressIncrements * Time.deltaTime;

        Color currentColor = _sprite.color;

        currentColor.a = (1 - Mathf.InverseLerp(0, 100, _progress));
        _sprite.color = currentColor;

        if (_progress >= 100.0f)
        {
            EventsManager.Instance.WallManchaCleared();
            audioSource.Stop();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Esponja")
        {
            esponjaComponent = other.gameObject.GetComponent<EsponjaComponent>();

            if (esponjaComponent.wet)
            {
                esponjaComponent.ActivateCleaningParticles();
                audioSource.Play();
            }
            
            esponjaCleaning = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Esponja")
        {
            esponjaComponent = other.gameObject.GetComponent<EsponjaComponent>();
            esponjaComponent.DeactivateCleaningParticles();

            esponjaCleaning = false;
            audioSource.Stop();
        }
    }
}
