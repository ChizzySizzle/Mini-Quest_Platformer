using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Player_Controller : MonoBehaviour
{
    [Header("Object References")]
    public Transform head;
    public GameObject firePoint;
    public GameObject bullet;

    [Header("Gameplay Variables")]
    public float health = 10f;
    public float speed = 1f;
    public float maxSpeed = 10f;
    public float jumpForce = 1f;
    public float horizontalSens = 2.0f;
    public float fireRate = .3f;

    // Private Variables
    private Vector3 moveVector;
    private Rigidbody rb;
    private Quaternion horizontalRotation;
    private float lastFire;
    private bool isGrounded;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float h = horizontalSens * Input.GetAxis("Mouse X");
        
        horizontalRotation = Quaternion.Euler(0, h, 0);
        horizontalRotation = rb.rotation * horizontalRotation;
        rb.MoveRotation(horizontalRotation);

        Vector3 moveDirection = transform.right * moveVector.x + transform.forward * moveVector.z;

        if (rb.velocity.magnitude < maxSpeed) {
            rb.AddForce(moveDirection * speed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }

    public void SetHealth(float healthDelta) {
        health += healthDelta;
        
        if (health <= 0) {
            Debug.Log("You Have Died");
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
