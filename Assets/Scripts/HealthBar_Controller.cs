using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class HealthBar_Controller : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void UpdateHealthbar(float newValue) {
        newValue = newValue / 10;
        slider.value = newValue;
    }
}
