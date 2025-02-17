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
    public Barrier_Controller barrier;

    
    private GameObject player;
    private float lastFire = 0f;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Projectile")) {
            if (collider.GetComponent<Bullet_Controller>() != null) { 
                Bullet_Controller bullet = collider.GetComponent<Bullet_Controller>();
                SetHealth(bullet.bulletDamage);
            }
        }
    }

    public void SetHealth(float damage) {
        health -= damage;

        if (health <= 0) {
            DestroySelf();
        }
    }

    void DestroySelf() {
        if (barrier != null)
            barrier.BarrierCheck();
        Destroy(gameObject);
    }
}
