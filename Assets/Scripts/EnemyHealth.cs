using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints= 100;
    [SerializeField] AudioClip deadSound;
    Animator anim;
    bool isDead;
    public bool IsDead { get { return isDead; } }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        GetComponent<EnemyAI>().OnDamageTaken();
        //BroadcastMessage("OnDamageTaken");
        hitPoints = hitPoints - damage;
    }

    private void Update()
    {
        if(hitPoints <= 0)
        {
            if(!isDead)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        GetComponent<AudioSource>().PlayOneShot(deadSound);
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();
        CapsuleCollider[] capsuleColliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (BoxCollider collider in boxColliders)
        {
            collider.enabled = false;
        }
        foreach (CapsuleCollider collider in capsuleColliders)
        {
            collider.enabled = false;
        }
        anim.SetTrigger("die");
        // Destroy(gameObject);
    }


}
