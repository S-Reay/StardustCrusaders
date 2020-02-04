using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;
    public Rigidbody rb;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }
}
