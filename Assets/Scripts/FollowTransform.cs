using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform other;
    void Update()
    {
        transform.position = other.position;
    }
}
