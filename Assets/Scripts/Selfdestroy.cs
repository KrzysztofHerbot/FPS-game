using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestroy : MonoBehaviour
{
    [SerializeField] float timeToSelfdestroy = 1f;
    void Start()
    {
        Invoke("Destruct", timeToSelfdestroy);
    }

    void Destruct()
    {
        Destroy(gameObject);
    }

}
