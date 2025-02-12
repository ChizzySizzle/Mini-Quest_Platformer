using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    public float chaseRange = 10f;
    public float health = 10f;

    private NavMeshAgent agent;
    private GameObject player;
    
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

    public void SetHealth(float damage) {
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
