using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AnimateHands : MonoBehaviour
{
    public GameObject handPrefab;
    public InputDeviceCharacteristics inputDeviceCharacteristics;

    private InputDevice _targetDevice;
    private Animator _animator;
    private GameObject _handGameObject;

    private bool handHidden;

    public Material normalMat;
    public Material dingMat;
    public bool dingHand;
    private AudioSource audioSource;
    public void HideHand()
    {
        _handGameObject.SetActive(false);
        handHidden = true;
    }
    public void ShowHand()
    {
        _handGameObject.SetActive(true);
        handHidden = false;
    }

    

    private void InitializeHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        if (devices.Count > 0)
        {
            _targetDevice = devices[0];

            _handGameObject = Instantiate(handPrefab, transform);
            _animator = _handGameObject.GetComponent<Animator>();
        }
    }

    private void UpdateHand()
    {
        if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            _animator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            _animator.SetFloat("Trigger", 0);
        }

        if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _animator.SetFloat("Grip", gripValue);
        }
        else
        {
            _animator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!handHidden)
        {
            if (!_targetDevice.isValid)
                InitializeHand();
            else
                UpdateHand();
        }
    }

    private void DingHand()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = SoundManager.Instance.GetAudioClip(SoundManager.SFX.Ding);
        audioSource.Play();

        setDingMaterial();
        Invoke("setNormalMaterial", 0.3f);
        Invoke("setDingMaterial", 0.6f);
        Invoke("setNormalMaterial", 0.9f);
    }

    private void setNormalMaterial()
    {
        _handGameObject.GetComponentInChildren<Renderer>().material = normalMat;
    }

    private void setDingMaterial()
    {
        _handGameObject.GetComponentInChildren<Renderer>().material = dingMat;
    }

    // Start is called before the first frame update
    void Start()
    {
        handHidden = false;
        InitializeHand();

        if (dingHand)
        {
            EventsManager.Instance.OnHandMenuUpdated += DingHand;
        }
    }
}
