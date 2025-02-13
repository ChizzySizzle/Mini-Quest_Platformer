using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Bullet_Controller : MonoBehaviour
{
    public float bulletSpeed = 30f;
    public float bulletDamage = 2f;
    private Rigidbody rb;
    private Player_Controller player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = FindObjectOfType<Player_Controller>();
        Rigidbody p_rb = player.GetComponent<Rigidbody>();
        Vector3 p_Vel = new Vector3(p_rb.velocity.x, 0, p_rb.velocity.z);
        
        rb.velocity = transform.forward * bulletSpeed + p_Vel;
        Invoke("DestroySelf", 2f);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            DestroySelf();
        }
        if (collision.gameObject.CompareTag("Ground")) {
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
