using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Controller : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float healthIncrease = 1;
    public ParticleSystem pickupEffect;
    
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }
}
