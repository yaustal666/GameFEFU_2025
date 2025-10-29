using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OutOfHealth;
    public float maxHealth;
    public float currentHealth;

    public void GetDamage(float damage)
    {
        if (currentHealth <= damage)
        {
            OutOfHealth.Invoke();
            currentHealth = 0f;
        }
        else
        {
            currentHealth -= damage;
        }
    }

    public void Heal(float hp)
    {
        currentHealth = hp;
        if (currentHealth > maxHealth) { 
            currentHealth = maxHealth;
        }
    }
}