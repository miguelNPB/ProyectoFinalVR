using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorEvent : MonoBehaviour
{
    public GameObject model;
    private Camera mainCamera;
    public SoundManager.SFX soundClip;
    public float eventTime;
    public float cameraAngle = 40f;

    private Animator animator;
    private Transform modelTransform;
    private AudioSource audioSource;

    private bool done;

    private void SelfDestroy()
    {
        Destroy(model);
        Destroy(gameObject);
    }
    private void Start()
    {
        done = false;
        animator = model.GetComponent<Animator>();
        audioSource = model.GetComponent<AudioSource>();
        audioSource.clip = SoundManager.Instance.GetAudioClip(soundClip);
        modelTransform = model.transform;
        mainCamera = Camera.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 directionToTarget = (modelTransform.position - mainCamera.transform.position).normalized;
        if (other.gameObject.tag == "Player" && Vector3.Angle(directionToTarget, mainCamera.transform.forward) < cameraAngle && !done)
        {
            done = true;
            audioSource.Play();
            animator.SetTrigger("Trigger");
            Invoke("SelfDestroy", eventTime);
        }
    }
}
