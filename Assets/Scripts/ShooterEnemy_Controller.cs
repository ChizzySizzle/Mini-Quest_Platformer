
using UnityEngine;

public class ShootEnemy_Controller : MonoBehaviour
{
    // Variables to adjust enemy behavior
    public float enemyRange = 10f;
    public float fireRate = .5f;
    public float health = 4f;

    // Scene object references
    public GameObject firePoint;
    public GameObject bullet;
    public AudioClip shootSound;
    public AudioClip hitNoise;
    public Barrier_Controller barrier;

    // private variables for runtime calculations
    private GameObject player;
    private new AudioSource audio;
    private float lastFire = 0f;

    // Find the player based on tag, define the audio source component
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();
    }

    // Fire at the player if they are within range
    void FixedUpdate() {
        // find the distance between enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // If the player is within range, begin firing
        if (distanceToPlayer <= enemyRange) {
            // Point gun at player
            transform.LookAt(player.transform.position);
            // Ensure enemy is not exceeding maximum fire rate
            // play shoot audio
            // Fire the gun
            if (Time.time - lastFire > fireRate) {
                audio.PlayOneShot(shootSound, 1.2f);
                Instantiate(bullet, firePoint.transform.position, transform.rotation);
                lastFire = Time.time;
            }
        }
    }

    // If the enemy gets hit by the players bullet: play hit noise, find the bullet's damage value and apply that to the enemy's health
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Projectile")) {
            if (collider.GetComponent<Bullet_Controller>() != null) { 
                audio.PlayOneShot(hitNoise, 0.8f);
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
