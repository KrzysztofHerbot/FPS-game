using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] float normalFOV = 60f;
    [SerializeField] float normalSensitivity = 2f;
    [SerializeField] float zoomFOV = 30f;
    [SerializeField] float zoomSensitivity = 1.5f;
    bool cameraToogle = false;
    FirstPersonController fpsController;
    //RigidbodyFirstPersonController fpsController2;

    void Start()
    {
        fpsController = GetComponent<FirstPersonController>();
        //fpsController2 = GetComponent<RigidbodyFirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (cameraToogle)
            {
                GetComponentInChildren<Camera>().fieldOfView = zoomFOV;
                cameraToogle = !cameraToogle;
                fpsController.m_MouseLook.XSensitivity = zoomSensitivity;
                fpsController.m_MouseLook.YSensitivity = zoomSensitivity;
            }
            else 
            { 
                GetComponentInChildren<Camera>().fieldOfView = normalFOV;
                cameraToogle = !cameraToogle;
                fpsController.m_MouseLook.XSensitivity = normalSensitivity;
                fpsController.m_MouseLook.YSensitivity = normalSensitivity;
            }
        }
    }
}
