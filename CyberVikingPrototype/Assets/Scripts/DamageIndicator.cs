using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public Transform camTransform;
    Quaternion originalRotation;

    public float riseSpeed = 0.5f;

    void Start()
    {

        camTransform = Camera.main.transform;
        originalRotation = transform.rotation;
        Destroy(gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;
        transform.position += new Vector3 (0f, Time.deltaTime * riseSpeed, 0f);
    }
}
