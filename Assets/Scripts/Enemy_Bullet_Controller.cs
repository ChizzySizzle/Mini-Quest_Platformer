using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Controller : MonoBehaviour
{
    // Variables to change bullet behavior
    public float bulletSpeed = 30f;
    public float bulletDamage = -1f;

    private Rigidbody rb;

    // Get the rigidbody component
    // Apply a force in the direction the enemy is facing
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletSpeed;

        Invoke("DestroySelf", 2f);
    }

    // Call destory self if the bullet collides with the floor or the player
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            DestroySelf();
        }
        if (collision.gameObject.CompareTag("Ground")) {
            DestroySelf();
        }
    }
    
    // Used to destroy itself :)
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
