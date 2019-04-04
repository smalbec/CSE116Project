using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealthBar : MonoBehaviour
{
    //created slider and wall variables so healthbar is independent to each wall
    public Slider healthBar;
    public Wall wall;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = 150;  
    }

    // Update is called once per frame
    void Update()
    {
        //Change the healthBar slider to the wall health
        healthBar.value = wall.health;
        //Checks to see if the wall object exists (null if wall is destroyed)
        if(wall == null)
        {
            Destroy(gameObject);
            Debug.Log("Yeeticus");
        }
    }
}
