using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Bullet_Controller : MonoBehaviour
{
    // Object behavior variables
    public float bulletSpeed = 30f;
    public float bulletDamage = 2f;

    // Private vareiables for runtime calculations
    private Rigidbody rb;
    private Player_Controller player;

    // Get the rigidbody component and the player object
    // get the players current speed and add it to the bullets initial velocity
    // Apply the initial force in the direction the player is facing
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = FindObjectOfType<Player_Controller>();
        Rigidbody p_rb = player.GetComponent<Rigidbody>();
        Vector3 p_Vel = new Vector3(p_rb.linearVelocity.x, 0, p_rb.linearVelocity.z);
        
        rb.linearVelocity = transform.forward * bulletSpeed + p_Vel;

        //Destroy the bullet after 2 seconds of travel
        Invoke("DestroySelf", 2f);
    }

    // Call destory self if the bullet collides with the floor or an enemy
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
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
