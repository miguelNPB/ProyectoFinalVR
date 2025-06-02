using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchGrabType : MonoBehaviour
{
    public InputActionReference Xleft;
    public InputActionReference Bright;
    public GameObject leftDirect;
    public GameObject leftRay;
    public GameObject rightDirect;
    public GameObject rightRay;

    private bool on;
    private void SwitchType(InputAction.CallbackContext context)
    {

    }

    private void OnEnable()
    {
        Xleft.action.performed += SwitchType;
        Bright.action.performed += SwitchType;
    }

    private void OnDisable()
    {
        Xleft.action.performed -= SwitchType;
        Bright.action.performed -= SwitchType;
    }

    void Start()
    {
        on = true;
    }
}
