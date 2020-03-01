using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 200;

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
