using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [Header("Object References")]
    public Transform head;

    public float speed = 1f;
    public float jumpForce = 1f;
    public float LookSenseX = 2.0f;
    public float LookSenseY = 2.0f;
    private Vector3 moveVector;


    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        float h = LookSenseX * Input.GetAxis("Mouse X");
        float v = LookSenseY * Input.GetAxis("Mouse Y");
        head.transform.Rotate(-v, h, 0);
        transform.Rotate(0, h, 0);
    }

    void OnMovement(InputValue moveValue) {
        
    }

    void OnJump() {

    }
}
