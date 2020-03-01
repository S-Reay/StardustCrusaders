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

    public GameObject slashObject;
    public Transform slashOrigin;

    public GameObject grenadeObject;
    public Transform grenadeOrigin;
    public float grenadeForwardForce;
    public float grenadeUpForce;

    EquipmentManager equip;
    public Weapon currentWeapon;

    public float attackDelay;

    public LineRenderer laserLineRenderer;
    public float laserWidth = 0.1f;
    public float laserMaxLength = 100000f;
    public float laserFade = 0f;
    public float maxLaserFade = 0.6f;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
        equip = EquipmentManager.instance;
        equip.onWeaponChangedCallback += UpdateWeapon;

        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;
    }

    void UpdateWeapon(Weapon newWeapon, Weapon oldWeapon)
    {
        currentWeapon = newWeapon;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RangedAttack();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            MeleeAttack();
        }

        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
        if (laserFade > 0f && laserLineRenderer.enabled == true)
        {
            laserFade -= Time.deltaTime;
            laserLineRenderer.startWidth = laserWidth * (laserFade / maxLaserFade);
            laserLineRenderer.endWidth = laserWidth * (laserFade / maxLaserFade);
        }
        else
        {
            laserLineRenderer.enabled = false;
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

    void RangedAttack()
    {
        if (currentWeapon != null)              //If there is an item equipped in the right hand
        {
            Debug.Log("Weapon found, attempting attack");
            switch (currentWeapon.rangedAttack) //Check the Ranged Attack Type
            {
                case RangedAttackType.Raycast:
                    RaycastAttack();
                    break;
                case RangedAttackType.Slash:
                    SlashAttack();
                    break;
                case RangedAttackType.Grenade:
                    GrenadeAttack();
                    break;
                default:
                    Debug.LogError("Could not determine ranged attack type of " + currentWeapon.name);
                    break;
            }
        }
    }

    void RaycastAttack()
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

        ShootLaserFromTargetPosition(transform.position, cam.transform.forward, laserMaxLength);
        laserLineRenderer.enabled = true;
        laserFade = maxLaserFade;
    }

    void SlashAttack()
    {
        GameObject newSlash = Instantiate(slashObject, slashOrigin.position, cam.transform.rotation);
        newSlash.GetComponent<SlashBehaviour>().SetDamage(stats.damage.GetValue());
    }

    void GrenadeAttack()
    {
        GameObject newGrenade = Instantiate(grenadeObject, grenadeOrigin.transform.position, Quaternion.identity);
        Rigidbody rb = newGrenade.GetComponent<Rigidbody>();
        newGrenade.GetComponent<GrenadeBehaviour>().SetDamage(stats.damage.GetValue());
        rb.AddForce((Camera.main.transform.forward * grenadeForwardForce) + Vector3.up * grenadeUpForce, ForceMode.Impulse);
    }

    void MeleeAttack()
    {

    }

    void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction, float length)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(ray, out raycastHit, length))
        {
            endPosition = raycastHit.point;
        }

        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
    }
}
