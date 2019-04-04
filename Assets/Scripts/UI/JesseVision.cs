using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JesseVision : MonoBehaviour
{
    public Image image;
 
    // Use this for initialization
    void Start () 
    {
        image = GetComponent<Image> ();
    }
     
    // Update is called once per frame
    void Update () 
    {
        image.color = new Color(1,1,1,JesseSlider.newAlpha);
        if(JesseSlider.newAlpha >= 0.85f){
            image.color = new Color(1,1,1,0.85f);
        }
        Debug.Log(JesseSlider.newAlpha);
    }
}
