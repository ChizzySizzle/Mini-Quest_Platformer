
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [Header("Object References")]
    public Transform head;
    public GameObject firePoint;
    public GameObject bullet;
    public HealthBar_Controller healthBar;

    [Header("Gameplay Variables")]
    public float health = 10f;
    public float acceleration = 1f;
    public float maxSpeed = 10f;
    public float jumpForce = 1f;
    public float horizontalSens = 2.0f;
    public float fireRate = .3f;

    // Private Variables
    private Game_Controller gameController;
    private Vector3 moveVector;
    private Rigidbody rb;
    private Quaternion horizontalRotation;
    private float lastFire;
    private bool isGrounded;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        gameController = FindObjectOfType<Game_Controller>();
    }

    void FixedUpdate() {
        float h = horizontalSens * Input.GetAxis("Mouse X");
        
        horizontalRotation = Quaternion.Euler(0, h, 0);
        horizontalRotation = rb.rotation * horizontalRotation;
        rb.MoveRotation(horizontalRotation);

        Vector3 moveDirection = transform.right * moveVector.x + transform.forward * moveVector.z;

        if (rb.velocity.magnitude < maxSpeed) {
            rb.AddForce(moveDirection * acceleration);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Projectile")) {
            SetHealth(trigger.GetComponent<Enemy_Bullet_Controller>().bulletDamage);
        }

        if (trigger.CompareTag("Boundary")) {
            gameController.EndGame("Lose");
        }

        if (trigger.CompareTag("Pickup")) {
            Pickup_Controller pickup = trigger.GetComponent<Pickup_Controller>();
            SetHealth(pickup.healthIncrease);
            Instantiate(pickup.pickupEffect, gameObject.transform.position, gameObject.transform.rotation);
            pickup.DestroySelf();
        }

        if (trigger.gameObject.CompareTag("Goal")) {
            gameController.EndGame("Win");   
        }
    }

    public void SetHealth(float healthDelta) {
        health += healthDelta;

        healthBar.UpdateHealthbar(health);

        if (health <= 0) {
            gameController.EndGame("Lose");
        }
    }

    public void OnMovement(InputValue movementValue) {
        moveVector = movementValue.Get<Vector3>();
    }

    public void OnJump() {
        if (isGrounded) {
            rb.AddForce(new Vector3(0, 1 * jumpForce, 0), ForceMode.Impulse);
        }
        isGrounded = false;
    }

    public void OnFire() {
        if (Time.time - lastFire > fireRate) {
            Instantiate(bullet, firePoint.transform.position, horizontalRotation);
            lastFire = Time.time;
        }
    }
}
