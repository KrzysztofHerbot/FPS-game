using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] TextMeshProUGUI HPnumber;
    [SerializeField] TextMeshProUGUI MedkitsText;
    [SerializeField] AudioClip healSound;
    [SerializeField] AudioClip pickMedkitSound;
    AudioSource ac;
    int medkits = 0;
    int healthMed = 40;
    DeathHandler deathHandler;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        deathHandler = GetComponent<DeathHandler>();
        MedkitsText.text = medkits.ToString();
        ac = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HPnumber.text = currentHealth.ToString();
        if (Input.GetKeyDown(KeyCode.H) && currentHealth < maxHealth && medkits > 0)
        {
            UseMedkit();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthPack>() != null)
        {
            Destroy(other.gameObject);
            medkits++;
            MedkitsText.text = medkits.ToString();
            ac.PlayOneShot(pickMedkitSound);
            
        }else return;
        
    }

    void UseMedkit()
    {
        medkits--;
        MedkitsText.text = medkits.ToString();
        GetHealth(healthMed);
    }

    void GetHealth(int health)
    {
        ac.PlayOneShot(healSound);
        if (currentHealth + health < maxHealth)
        {
            currentHealth = currentHealth + health;
        }
        else currentHealth = maxHealth;
    }

    public void PlayerTakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        if(currentHealth <= 0)
        {
            ProcessPlayerDeath();
        }
    }

    void ProcessPlayerDeath()
    {
        Debug.Log("You died.");
        deathHandler.HandleDeath();
    }
}
