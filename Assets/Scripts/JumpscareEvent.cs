using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class JumpscareEvent : MonoBehaviour
{
    public GameObject panelIrADormir;
    public GameObject modelJumpscare;
    public TMP_Text textIrADormir;
    private AudioSource audioSource;

    private bool allTasksDone;
    private bool jumpscareReady;
    private bool jumpscareDone;
    private void EnableJumpscare()
    {
        allTasksDone = true;
        panelIrADormir.SetActive(true);
    }
    private void EndGame()
    {
        SceneManager.LoadScene(0);
    }
    private void Start()
    {
        allTasksDone = false;
        jumpscareReady = false;
        jumpscareDone = false;
        audioSource = modelJumpscare.GetComponent<AudioSource>();
        EventsManager.Instance.OnAllTasksDone += EnableJumpscare;
    }
    public void Update()
    {
        if (jumpscareReady)
        {
            Vector3 directionToTarget = (modelJumpscare.transform.position - Camera.main.transform.position).normalized;
            if (Vector3.Angle(directionToTarget, Camera.main.transform.forward) < 80f && !jumpscareDone)
            {
                modelJumpscare.GetComponent<Animator>().SetTrigger("Trigger");
                audioSource.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.Jumpscare);
                audioSource.Play();

                Invoke("EndGame",1.1f);
                jumpscareDone = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && allTasksDone)
        {
            textIrADormir.text = "Mira detras tuya";

            GameManager.Instance.locomotion.SetActive(false);
            modelJumpscare.SetActive(true);
            modelJumpscare.transform.LookAt(other.transform);
            audioSource.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.Breathing);
            audioSource.Play();

            jumpscareReady = true;
        }
    }
}
