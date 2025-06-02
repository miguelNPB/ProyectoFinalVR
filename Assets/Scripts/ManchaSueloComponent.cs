using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManchaSueloComponent : MonoBehaviour
{
    public float progressIncrements = 10f;

    private float _progress;
    private SpriteRenderer _sprite;

    private float maxProgress;

    private AudioSource audioSource;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.SweepFloor);
        _progress = 0;
        maxProgress = _sprite.color.a;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Broom")
        {
            _progress += progressIncrements;

            Color currentColor = _sprite.color;

            currentColor.a = (1 - Mathf.InverseLerp(0, maxProgress * 100, _progress));
            _sprite.color = currentColor;

            if (_progress >= 100.0f)
            {
                EventsManager.Instance.FloorManchaCleared();
                Destroy(gameObject);
            }

            audioSource.Play();
        }
    }
}
