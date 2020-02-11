using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat maxHealth;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armour;
    public Stat moveSpeed;
    public Stat jumpHeight;

    private void Awake()
    {
        currentHealth = maxHealth.GetValue();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(10);
        }
    }

    public int TakeDamage(int damage)
    {
        damage -= armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);  //Ensure damage never drops below zero which would cause healing on hit

        currentHealth -= damage;
        Debug.Log(transform.name + " took " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
        return damage;
    }

    public virtual void Die()
    {
        //This method is meant to be overwritten
        //DIE DEPENDING ON WHO IT IS

        Debug.Log(transform.name + " died.");
        Destroy(gameObject);
    }
}
