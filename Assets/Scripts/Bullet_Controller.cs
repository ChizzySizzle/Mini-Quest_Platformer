using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Bullet_Controller : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody rb;
    private Player_Controller player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = FindObjectOfType<Player_Controller>();
        Rigidbody p_rb = player.GetComponent<Rigidbody>();
        Vector3 p_Vel = new Vector3(p_rb.velocity.x, 0, p_rb.velocity.z);
        
        rb.velocity = transform.forward * bulletSpeed + p_Vel;
        Invoke("DestroySelf", 2f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
