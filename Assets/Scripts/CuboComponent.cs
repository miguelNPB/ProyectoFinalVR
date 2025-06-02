using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboComponent : MonoBehaviour
{
    [SerializeField] private GameObject WaterLayer;
    [SerializeField] private GameObject WaterCollider;

    private bool done;
    private AudioSource audioSource;

    private void Start()
    {
        done = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.BucketFill);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water" && !done)
        {
            WaterLayer.SetActive(true);
            WaterCollider.SetActive(true);
            audioSource.Play();
            done = true;
        }
    }
}
