using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeBehaviour : MonoBehaviour
{
    public float fuseTimer = 2.5f;
    public float blastRadius = 5f;
    public GameObject damageIndicator;
    public GameObject explosionEffect;
    bool hasExploded = false;
    int damage;

    public void SetDamage(int amount)
    {
        damage = amount;
    }

    private void Update()
    {
        if (fuseTimer > 0 )
        {
            fuseTimer -= Time.deltaTime;
        }
        else if (!hasExploded)
        {
            Detonate();
            hasExploded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Detonate();
        }
    }

    void Detonate()
    {
        Debug.Log("BOOM");
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyCollider in colliders)
        {
            EnemyStats enemyStats = nearbyCollider.GetComponent<EnemyStats>();
            if (enemyStats != null)  //If it's an enemy
            {
                GameObject newIndicator = Instantiate(damageIndicator, nearbyCollider.transform.position, Quaternion.identity);
                newIndicator.GetComponentInChildren<Text>().text = enemyStats.TakeDamage(damage).ToString();
            }
        }

        //PLAY SOUND
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject); 
    }
}
