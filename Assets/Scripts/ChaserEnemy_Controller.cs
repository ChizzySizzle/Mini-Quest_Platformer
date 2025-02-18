using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    // Variables to adjust enemy behavior
    public float chaseRange = 10f;
    public float health = 10f;
    public float hitDamage = 1f;
    public float hitCooldown = .5f;

    // Scene object references
    public Barrier_Controller barrier;
    public Animator animationController;
    public AudioClip hitNoise;

    // private variables for runtime calculations
    private NavMeshAgent agent;
    private GameObject player;
    private new AudioSource audio;
    private float lastHit;
    
    // Find the player based on tag, define the audio source and navmesh agent components
    void Start() {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
    }

    // Chase the player when they are in range
    void FixedUpdate() {
        // Findx the distance from enemy to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // If the player is within range, begin chasing
        if (distanceToPlayer <= chaseRange) {
            // Navmesh function to move the enemy towards the player
            agent.SetDestination(player.transform.position);
            // Set the running boolean to true for the animation controller
            animationController.SetBool("IsRunning", true);
        }
        else {
            // Set the running boolean to false for the animation controller
            animationController.SetBool("IsRunning", false);
        }
    }

    // Hurt the player on collision with the enemy
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            // Wait for the cooldown before hurting the player again
            if (Time.time - lastHit > hitCooldown) {
                // Change the players health by hit damage value
                player.GetComponent<Player_Controller>().SetHealth(hitDamage);
                lastHit = Time.time;
            }
        }
    }

    // If the enemy gets hit by the players bullet: play hit noise, find the bullet's damage value and apply that to the enemy's health
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Projectile")) {
            if (collider.GetComponent<Bullet_Controller>() != null) { 
                audio.PlayOneShot(hitNoise, .8f);
                Bullet_Controller bullet = collider.GetComponent<Bullet_Controller>();
                SetHealth(bullet.bulletDamage);
            }
        }
    }

    // Set health based on input
    public void SetHealth(float damage) {
        health -= damage;

        // Enemy has died
        if (health <= 0) {
            DestroySelf();
        }
    }

    // Send death message to the enemies barrier
    // destroy self
    void DestroySelf() {
        if (barrier != null)
            barrier.BarrierCheck();
        Destroy(gameObject);
    }
}
