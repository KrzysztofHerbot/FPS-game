using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    CharacterController control;
    Animator anim;
    void Start()
    {
        control = GetComponentInParent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("SPEED: " + control.velocity.magnitude);
        if(control.velocity.magnitude < 0.3f )
        {
            anim.SetBool("isStanding", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isSprinting", false);
        }
        else if (control.velocity.magnitude > 0.3f && control.velocity.magnitude < 5f)
        {
            anim.SetBool("isStanding", false);
            anim.SetBool("isRunning", true);
            anim.SetBool("isSprinting", false);
        }
        else if (control.velocity.magnitude >= 5f)
        {
            anim.SetBool("isStanding", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isSprinting", true);
        }
    }
}
