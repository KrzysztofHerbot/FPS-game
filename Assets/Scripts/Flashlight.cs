using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject flashlight;
    [SerializeField] AudioClip flashlightSound;
    bool isOn;
    AudioSource ac;
    void Start()
    {
        ac = GetComponent<AudioSource>();
        isOn = true;
        flashlight.SetActive(isOn);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ac.PlayOneShot(flashlightSound);
            isOn = !isOn;
            flashlight.SetActive(isOn);
        }
    }
}
