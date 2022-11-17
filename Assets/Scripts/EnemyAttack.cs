using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] AudioClip zombieAttack;
    AudioSource ac;
    PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        ac = GetComponent<AudioSource>();
        ac.PlayOneShot(zombieAttack);
        if (playerHealth == null) return;
        playerHealth.PlayerTakeDamage(damage);
        Debug.Log("You've got hitted for " + damage + " damage!");
    }
}
