using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    public float damage = 10f;
    public float weaponRange = 500f;
    public float interactionRange = 1.8f;

    public Camera cam;
    public ParticleSystem muzzleFlash;
    public GameObject damageIndicator;

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
        muzzleFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weaponRange))
        {
            Debug.Log("Hit: " + hit.transform.name);

            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                //SHOW DAMAGE INDICATOR
                GameObject newIndicator = Instantiate(damageIndicator, hit.point, Quaternion.identity);
                newIndicator.GetComponentInChildren<Text>().text = damage.ToString();
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
