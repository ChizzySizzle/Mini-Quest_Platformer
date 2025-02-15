using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    public float chaseRange = 10f;
    public float health = 10f;
    public float hitDamage = 1f;
    public float hitCooldown = .5f;

    private NavMeshAgent agent;
    private GameObject player;
    private float lastHit;
    
    void Start() {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate() {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= chaseRange) {
            agent.SetDestination(player.transform.position);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            if (Time.time - lastHit > hitCooldown) {
                player.GetComponent<Player_Controller>().SetHealth(hitDamage);
                lastHit = Time.time;
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
        Destroy(gameObject);
    }
}
