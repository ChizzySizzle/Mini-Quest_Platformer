using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class HealthBar_Controller : MonoBehaviour
{
    private Slider slider;

    // Get slider component
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Update the healthbar appearance based on the players new health value
    public void UpdateHealthbar(float newValue) {
        // Divide by ten to map player health values between the domain [0 , 1]
        newValue = newValue / 10;
        // set the slider value to the new value
        slider.value = newValue;
    }
}
