using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerhealth : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth = 1;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount) 
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void ReceberDano(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

