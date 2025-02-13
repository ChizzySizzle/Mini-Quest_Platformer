using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Controller : MonoBehaviour
{
    public float bulletSpeed = 30f;
    public float bulletDamage = -1f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;

        Invoke("DestroySelf", 2f);
    }

        void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
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
