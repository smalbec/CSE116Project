using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player: MonoBehaviour{

    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
   
 
    static public Size vore_2 = new Size();
    static public Size vore_1 = new Size();
    static public Traits person = new Traits();
   

    // Use this for initialization
    void Start () {
        person.size = 1.0f;
        person.rate = 0.1f;
        person.health = 100f;
        person.base_damage = 25f;
        person.damage = 0f;
        person.projectile_lifetime = 1f;
        rb = GetComponent<Rigidbody2D>();	
	}
    public static void Compare() //this function makes sure the kill count can't be a negative number.  this shouldn't be possible but just in case
    { 
        if (vore_2.kill < 0) //if kill count is less than 0, it just sets it to zero
        {
            vore_2.kill = 0;
        }
        else if (vore_1.kill < 0) //same with this, although vore_1.kill depends on vore_2.kill.
        {
            vore_1.kill = 0;
        }
        else
        {
           
        }
    }
    public static void Damage() //when called, this updates the current damage, the rate could be set in Traits (25)
    {
        Compare(); //called in each successive function as a failsafe, could be removed for efficiency
        person.damage = person.base_damage + 25 * vore_2.kill;
    }
        
    public static void Upsize() //increases the size of the player based on kill count
    {
        Compare(); //same reason as Damage() 
        person.health += (vore_2.kill - vore_1.kill)*25;
        vore_1.kill = vore_2.kill; //this is a way to have this consistently checked, otherwise it could be a fixed update
        person.size = Convert.ToSingle(1 / Math.Log(vore_1.kill + 1.5, 2)); //this changes the "size," which is really just the multiplier for speed
    }
    public static void Degen() //health degeneration
    {
        if (person.health <= 0.0f) //health degeneration / damage might push it below zero
        {
            //Player goes to death screen
            SceneManager.LoadScene(3);

            person.size = 0.0f; //this destroys speed
            person.health = 0.0f; //so it's constantly in the zero loop
        }
        person.health -= person.size * person.rate; //this is just the degeneration function, called each frame.  Rate is arbitrary
        person.health = Mathf.Round(person.health * 100f) / 100f; //Chucc did this?
        // We can balance these based on kill count, so someone who has 3 kills doesn't experience degeneration or such.  Can be done in void update()
    }

    // Update is called once per frame
    void Update () {
        Damage(); //Called once per frame, but can be realistically be a fixed update
        if (vore_1.kill != vore_2.kill) //Just a work around to change the in-game size
        {
            Upsize();
            transform.localScale += new Vector3(0.1f, 0.1f, 0); //Changes the visible size, including collision boundaries in the form (x,y,z)
        }
        Degen(); //Needs to be called once per frame
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // This takes input in the form of arrow keys and WASD
        moveVelocity = moveInput.normalized * speed; //This determines direction of the velocity
        bool v = moveVelocity != new Vector2(0, 0) || speed != 0; //To make sure that the speed isn't zero
        if (v)
        {
            moveVelocity = moveVelocity * person.size;//Unity Discord said weird things happen with zero speed, they might be wrong
        }
	}

    void FixedUpdate()//Could put damage, size, etc in here to save input lag
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);//Changes position on map based on velocity 
    }

}
