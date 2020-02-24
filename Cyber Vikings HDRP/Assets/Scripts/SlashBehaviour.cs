using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlashBehaviour : MonoBehaviour
{
    public float speed;
    public float lifespan;
    public int damage;
    public GameObject damageIndicator;

    public List<GameObject> previouslyHit = new List<GameObject>();

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }
    public void SetDamage(int amount)
    {
        damage = amount;
    }
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyStats enemyStats = other.transform.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            GameObject newIndicator = Instantiate(damageIndicator, other.transform.position, Quaternion.identity);
            newIndicator.GetComponentInChildren<Text>().text = enemyStats.TakeDamage(damage).ToString();
            previouslyHit.Add(other.gameObject);
        }

    }
}
