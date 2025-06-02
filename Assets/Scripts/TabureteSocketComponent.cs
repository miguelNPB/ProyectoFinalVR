using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabureteSocketComponent : MonoBehaviour
{
    public void Positioned()
    {
        EventsManager.Instance.ChairPositioned();
    }

    public void Unpositioned()
    {
        EventsManager.Instance.ChairUnPositioned();
    }
}
