using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsComponent : MonoBehaviour
{
    private AudioSource audioSource;
    private CharacterController chController;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.FootstepsPlayer);
        chController = GetComponent<CharacterController>();
        audioSource.Play();
        audioSource.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (chController.velocity.magnitude > 2)
            audioSource.UnPause();
        else
            audioSource.Pause();
    }
}
