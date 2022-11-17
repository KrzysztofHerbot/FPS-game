using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PressESC : MonoBehaviour
{
    [SerializeField] Canvas esc;
    bool isON;
    void Start()
    {
        isON = false;
        esc.enabled = isON;
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isON = !isON;
            esc.enabled = isON;
            if (isON)
            {
                Debug.Log("IS ON");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GetComponent<CharacterController>().enabled = false;
                GetComponent<RigidbodyFirstPersonController>().enabled = false;
                GetComponent<FirstPersonController>().enabled = false;
                Time.timeScale = 0;
            }
            else
            {
                Debug.Log("IS NOT ON");
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GetComponent<CharacterController>().enabled = true;
                GetComponent<RigidbodyFirstPersonController>().enabled = true;
                GetComponent<FirstPersonController>().enabled = true;
                Time.timeScale = 1;
            }
        }

        
    }
}
