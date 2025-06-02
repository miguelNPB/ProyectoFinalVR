using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaTutorialComponent : MonoBehaviour
{
    public void Read()
    {
        EventsManager.Instance.ReadTutorialLetter();
    }
}
