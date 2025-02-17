using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier_Controller : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> Enemies;
    private int enemiesAlive;

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
