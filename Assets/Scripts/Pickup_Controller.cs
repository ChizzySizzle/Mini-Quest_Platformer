using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Controller : MonoBehaviour
{
    // Pickup behavior variables
    public float rotationSpeed = 1;
    public float healthIncrease = 1;

    // Reference to the particle system prefab
    public ParticleSystem pickupEffect;
    
    // rotate the pickup in fixed inciments
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }

    // Destroy the pickup when called
    public void DestroySelf() {
        Destroy(gameObject);
    }
}
