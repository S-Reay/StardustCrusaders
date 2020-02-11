using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public float weaponRange = 500f;
    public float interactionRange = 1.8f;

    PlayerStats stats;

    public Camera cam;
    public GameObject damageIndicator;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }

    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weaponRange))
        {
            Debug.Log("Hit: " + hit.transform.name);

            EnemyStats enemyStats = hit.transform.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                GameObject newIndicator = Instantiate(damageIndicator, hit.point, Quaternion.identity);
                newIndicator.GetComponentInChildren<Text>().text = enemyStats.TakeDamage(stats.damage.GetValue()).ToString();
            }
        }
    }

    void Interact()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactionRange))
        {
            Debug.Log("Attempting to interact with: " + hit.transform.name);

            Interactable interactable = hit.transform.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
