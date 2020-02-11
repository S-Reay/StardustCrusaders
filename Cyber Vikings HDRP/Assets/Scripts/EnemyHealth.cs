﻿using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 50;
    public Rigidbody rb;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {

    }
}