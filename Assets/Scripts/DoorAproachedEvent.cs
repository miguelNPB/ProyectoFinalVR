using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAproachedEvent : MonoBehaviour
{
    public GameObject cartaBefore;
    public GameObject cartaAfter;

    private bool done = false;
    private void TurnCarta()
    {
        Destroy(cartaBefore);
        cartaAfter.SetActive(true);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !done)
        {
            done = true;
            EventsManager.Instance.DoorAproached();

            Invoke("TurnCarta", 1f);
            
        }
    }
}
