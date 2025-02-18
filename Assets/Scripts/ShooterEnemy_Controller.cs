
using UnityEngine;

public class ShootEnemy_Controller : MonoBehaviour
{
    public float enemyRange = 10f;
    public float fireRate = .5f;
    public float health = 4f;
    public GameObject firePoint;
    public GameObject bullet;
    public AudioClip shootSound;
    public AudioClip hitNoise;
    public Barrier_Controller barrier;

    
    private GameObject player;
    private new AudioSource audio;
    private float lastFire = 0f;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= enemyRange) {
            transform.LookAt(player.transform.position);
            if (Time.time - lastFire > fireRate) {
                audio.PlayOneShot(shootSound, 1.2f);
                Instantiate(bullet, firePoint.transform.position, transform.rotation);
                lastFire = Time.time;
            }
        }
    }

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
