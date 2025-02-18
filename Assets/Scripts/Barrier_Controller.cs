using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier_Controller : MonoBehaviour
{
    // Create an inspector accessible list of enemies that dictate the status of the barrier
    [SerializeField]
    public List<GameObject> Enemies;
    private int enemiesAlive;

    // Public function for enemies to call when they are killed
    // Checks the list of enemies to see how many remain
    // when there is 1 enemy remaining, destroy the barrier (last killed enemy is not destroyed before it is counted)
    public void BarrierCheck()
    {
        enemiesAlive = 0;

        for (int i = 0; i < Enemies.Count; i++)
            if (Enemies[i] != null)
                enemiesAlive++;
        
        if (enemiesAlive < 2)
            Destroy(gameObject);
    }
}
