using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashBehaviour : MonoBehaviour
{
    public float speed;
    public float lifespan;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

    }
}
