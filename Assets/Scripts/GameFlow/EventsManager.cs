using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }


    public event Action OnHandMenuOpen;
    public void HandMenuOpen()
    {
        OnHandMenuOpen?.Invoke();
    }
    public event Action OnHandMenuClose;
    public void HandMenuClose()
    {
        OnHandMenuClose?.Invoke();
    }
    public event Action OnWallManchaCleared;
    public void WallManchaCleared()
    {
        OnWallManchaCleared?.Invoke();
    }
    public event Action OnFloorManchaCleared;
    public void FloorManchaCleared()
    {
        OnFloorManchaCleared?.Invoke();
    }
    public event Action OnChairPositioned;
    public void ChairPositioned()
    {
        OnChairPositioned?.Invoke();
    }
    public event Action OnChairUnPositioned;
    public void ChairUnPositioned()
    {
        OnChairUnPositioned?.Invoke();
    }
    public event Action OnDoorLocked;
    public void DoorLocked()
    {
        OnDoorLocked?.Invoke();
    }
    public event Action OnHandMenuUpdated;
    public void HandMenuUpdated()
    {
        OnHandMenuUpdated?.Invoke();
    }
    public event Action OnReadTutorialLetter;
    public void ReadTutorialLetter()
    {
        OnReadTutorialLetter?.Invoke();
    }
    public event Action OnDoorAproached;
    public void DoorAproached()
    {
        OnDoorAproached?.Invoke();
    }
    public event Action OnAllTasksDone;
    public void AllTasksDone()
    {
        OnAllTasksDone?.Invoke();
    }
}
