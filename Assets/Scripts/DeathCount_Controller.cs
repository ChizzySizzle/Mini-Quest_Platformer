using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathCount_Controller : MonoBehaviour
{
    private TMP_Text tmp;

    void Start()
    {
        tmp = GetComponent<TMP_Text>();
        UpdateDeaths();
    }

    void UpdateDeaths()
    {
        tmp.SetText($"Deaths: {Game_Controller.instance.GetDeaths()}");
    }
}
