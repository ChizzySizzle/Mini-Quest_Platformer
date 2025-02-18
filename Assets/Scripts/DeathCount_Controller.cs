using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCount_Controller : MonoBehaviour
{
    private TMP_Text tmp;

    // Get the text component and call update deaths on level load
    void Start()
    {
        tmp = GetComponent<TMP_Text>();
        UpdateDeaths();
    }

    // Sets the text to reflect the number of times the player has died
    void UpdateDeaths()
    {
        tmp.SetText($"Deaths: {Game_Controller.instance.GetDeaths()}");
    }
}
