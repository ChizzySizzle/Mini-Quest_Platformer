
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player_Controller : MonoBehaviour
{
    // References to game objects in the scene
    [Header("Object References")]
    public Transform head;
    public GameObject firePoint;
    public GameObject bullet;
    public HealthBar_Controller healthBar;
    public Animator animationController;
    public AudioClip shootSound;

    // Variables that affect player gameplay mechanics
    [Header("Gameplay Variables")]
    public float health = 10f;
    public float acceleration = 1f;
    public float maxSpeed = 10f;
    public float jumpForce = 1f;
    public float horizontalSens = 2.0f;
    public float fireRate = .3f;

    // Private Variables for runtime calculations
    private Game_Controller gameController;
    private Vector3 moveVector;
    private Rigidbody rb;
    private new AudioSource audio;
    private Quaternion horizontalRotation;
    private float lastFire;
    private bool isGrounded;
    private float startHealth;

    // Assign private variables and lock the players cursor to center screen
    void Start() {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked; // learned this from unity scripting API
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        gameController = FindObjectOfType<Game_Controller>();
        startHealth = health;
    }

    // Update player rigidbody physics and set animation controller speed variable 
    void FixedUpdate() {
        // Could not figure out new input system for mouse axis, so I am capturing the mouse movement with legacy input
        // multiply mouse input by the sensitivity variable
        float h = horizontalSens * Input.GetAxis("Mouse X");
        
        // Store the rotation change into a quaternion euler variable
        horizontalRotation = Quaternion.Euler(0, h, 0);

        // Multiply the rigidbody's current rotation by the rotation delta
        horizontalRotation = rb.rotation * horizontalRotation;

        // Rotate the rigidbody to the new calculated rotation
        rb.MoveRotation(horizontalRotation); // Found this rigidbody function in the unity scripting API

        // Move player based on moveVector variables specified in the OnMovement input function
        Vector3 moveDirection = transform.right * moveVector.x + transform.forward * moveVector.z;

        // Apply movement only when player speed is below max specified limit
        if (rb.linearVelocity.magnitude < maxSpeed) {
            // Apply move direction force multiplied by specified acceleration value
            if (isGrounded == true) {
                rb.AddForce(moveDirection * acceleration);
            }
            // if the player is in the air, apply 1/5th force
            else {
                rb.AddForce(moveDirection * acceleration / 5);
            }
        }
        
        // Update the animation controller speed variable
        animationController.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x) + Mathf.Abs(rb.linearVelocity.z));
    }

    // When player collides with the ground, set grounded variable to true for player and animation controller
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
            animationController.SetBool("IsGrounded", true);
        }
    }

    // When player leaves the ground, set grounded variable to false for player and animation controller
    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = false;
            animationController.SetBool("IsGrounded", false);
        }
    }

    // If the player collides with a trigger, compare the tag and execute the appropriate code
    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Projectile")) {

            // The player has been hit by an enemy projectile, lower health by the damage specified by the bullet
            SetHealth(trigger.GetComponent<Enemy_Bullet_Controller>().bulletDamage);
        }

        if (trigger.CompareTag("Boundary")) {

            // the player has fallen off of the map
            gameController.EndGame("Lose");
        }

        if (trigger.CompareTag("Pickup")) {

            // The player has picked up health, Increase player health by amount specified by the pickup
            Pickup_Controller pickup = trigger.GetComponent<Pickup_Controller>();
            SetHealth(pickup.healthIncrease);

            // Create health pickup particle effect and tell the pickup to destroy itself
            Instantiate(pickup.pickupEffect, gameObject.transform.position, gameObject.transform.rotation);
            pickup.DestroySelf();
        }

        if (trigger.gameObject.CompareTag("Goal")) {

            // The player has reached the finish
            gameController.EndGame("Win");   
        }
    }

    // Function to change the player's health based on the input
    public void SetHealth(float healthDelta) {
        health += healthDelta;

        // The player has been killed
        if (health <= 0) {
            gameController.EndGame("Lose");
        }
        
        // Players health has exceeded the limit
        if (health > startHealth){
            health = startHealth;
        }

        // Update the healthbar to reflect new health value
        healthBar.UpdateHealthbar(health);
    }

    // Capture movement vector based on player input
    public void OnMovement(InputValue movementValue) {
        moveVector = movementValue.Get<Vector3>();
    }

    // Apply upwards force to the player when jump key is pressed
    public void OnJump() {
        if (isGrounded) {
            rb.AddForce(new Vector3(0, 1 * jumpForce, 0), ForceMode.Impulse); // using impulse to mimic real jumping
        }
    }

    // Fire a bullet when the player hits the shoot key
    // Check to make sure the player is not exceeding maximum fire rate
    // Play shooting sound effect
    public void OnFire() {
        if (Time.time - lastFire > fireRate) {
            audio.PlayOneShot(shootSound, .5f);
            Instantiate(bullet, firePoint.transform.position, horizontalRotation);
            lastFire = Time.time;
        }
    }
}
