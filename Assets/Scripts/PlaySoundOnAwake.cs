using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnAwake : MonoBehaviour
{
    [SerializeField] AudioClip pickCrunch;
    AudioSource ac;
    void Start()
    {
        ac = GetComponent<AudioSource>();
        ac.PlayOneShot(pickCrunch);
    }

}
