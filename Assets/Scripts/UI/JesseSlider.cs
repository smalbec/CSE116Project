using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JesseSlider : MonoBehaviour
{
    public Slider mainSlider;
    public static float newAlpha = 0f;

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(delegate {ValueChangeCheck();});
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        //Debug.Log(mainSlider.value);
    }

    public void Update()
    {
        newAlpha = mainSlider.value;
    }
}
