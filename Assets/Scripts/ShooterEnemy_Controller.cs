using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootEnemy_Controller : MonoBehaviour
{
    public float enemyRange = 10f;
    public float fireRate = .5f;
    public float health = 4f;
    public GameObject firePoint;
    public GameObject bullet;

    
    private GameObject player;
    private Rigidbody rb;
    private float lastFire = 0f;


    void Start() {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= enemyRange) {
            transform.LookAt(player.transform.position);
            if (Time.time - lastFire > fireRate) {
                Instantiate(bullet, firePoint.transform.position, transform.rotation);
                lastFire = Time.time;
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) {
            Bullet_Controller bullet = collision.GetComponent<Bullet_Controller>();
            SetHealth(bullet.bulletDamage);
        }
    }

    public void SetHealth(float damage) {
        health -= damage;
        Debug.Log(health);

        if (health <= 0) {
            DestroySelf();
        }
    }

    void DestroySelf() {
        Destroy(gameObject);
    }
}
