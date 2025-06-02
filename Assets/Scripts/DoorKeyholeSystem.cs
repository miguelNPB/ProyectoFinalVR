using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorKeyholeSystem : MonoBehaviour
{
    private AudioSource doorBangingAS;
    private AudioSource doorLockedAS;

    public GameObject carta;

    private bool droppedCartaMadre = false;
    public void KeyInserted()
    {
        EventsManager.Instance.DoorLocked();
        doorLockedAS.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.LockDoor);
        doorLockedAS.Play();
    }

    private void StartDoorBanging()
    {
        if (!droppedCartaMadre)
        {
            droppedCartaMadre = true;
            doorBangingAS.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.DoorBanging);
            Invoke("StartAudioDoorBanging", 9f);
        }
    }
    private void StartAudioDoorBanging()
    {
        doorBangingAS.Play();
    }
    private void StopDoorBanging()
    {
        doorBangingAS.Stop();
        carta.GetComponent<Animator>().Play("Spawn");
    }

    void Start()
    {
        doorLockedAS = GetComponents<AudioSource>()[0];
        doorBangingAS = GetComponents<AudioSource>()[1];

        EventsManager.Instance.OnReadTutorialLetter += StartDoorBanging;
        EventsManager.Instance.OnDoorAproached += StopDoorBanging;

        droppedCartaMadre = false;
    }

}
