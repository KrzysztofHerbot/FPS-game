using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxEnemy : MonoBehaviour
{
    EnemyHealth enemyHealth;
    EnemyAI enemyAI;
    [SerializeField] float bodypartHitMultiplayer = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAI = GetComponent<EnemyAI>();
        if (enemyHealth==null)
        {
            enemyHealth = GetComponentInParent<EnemyHealth>();
        }
        if (enemyAI == null)
        {
            enemyAI = GetComponentInParent<EnemyAI>();
        }
    }

    public void TakeDamageBodypart(int damage)
    {
        if (enemyHealth == null) return;
        if (enemyAI == null) return;
        enemyAI.OnDamageTaken();
        //BroadcastMessage("OnDamageTaken");
        enemyHealth.TakeDamage((int)(damage * bodypartHitMultiplayer));
    }

}
