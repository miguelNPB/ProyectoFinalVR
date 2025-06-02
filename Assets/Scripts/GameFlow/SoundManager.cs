using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField] private AudioClip spongeDry;
    [SerializeField] private AudioClip spongeWet;
    [SerializeField] private AudioClip bucketFill;
    [SerializeField] private AudioClip cleaningWall;
    [SerializeField] private AudioClip sweepFloor;
    [SerializeField] private AudioClip lockDoor;
    [SerializeField] private AudioClip ding;
    [SerializeField] private AudioClip footstepsPlayer;
    [SerializeField] private AudioClip doorBanging;
    [SerializeField] private AudioClip running;
    [SerializeField] private AudioClip breathing;
    [SerializeField] private AudioClip jumpscare;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    
    public enum SFX { SpongeDry, SpongeWet, BucketFill, CleaningWall, SweepFloor, LockDoor, Ding, FootstepsPlayer, DoorBanging, Running, Breathing, Jumpscare }
    public AudioClip GetAudioClip(SFX sfx)
    {
        switch (sfx)
        {
            case SFX.SpongeDry:
                return spongeDry;
            case SFX.SpongeWet:
                return spongeWet;
            case SFX.BucketFill:
                return bucketFill;
            case SFX.CleaningWall:
                return cleaningWall;
            case SFX.SweepFloor:
                return sweepFloor;
            case SFX.LockDoor:
                return lockDoor;
            case SFX.Ding:
                return ding;
            case SFX.FootstepsPlayer:
                return footstepsPlayer;
            case SFX.DoorBanging:
                return doorBanging;
            case SFX.Running:
                return running;
            case SFX.Breathing:
                return breathing;
            case SFX.Jumpscare:
                return jumpscare;
        }

        return null;
    }
}
