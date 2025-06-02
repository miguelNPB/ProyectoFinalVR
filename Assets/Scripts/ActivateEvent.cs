using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEvent : MonoBehaviour
{
    public GameObject eventGameobject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            eventGameobject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
