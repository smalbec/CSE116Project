using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace wig {
    public class Player: MonoBehaviour{

        public float speed;
        private Rigidbody2D rb;
        public Vector2 moveVelocity;
   
 
        static public Size vore_2 = new Size();
        static public Size vore_1 = new Size();
        static public Traits person = new Traits();
        public static Player player = new Player();
        public static GameObject playa;

        // Use this for initialization
        void Start () {
            //transform.position = new Vector2(UnityEngine.Random.Range(-15f, 15f), UnityEngine.Random.Range(-15f, 15f));
            person.size = 1.0f;
            person.rate = 0.1f;
            person.health = 100f;
            person.base_damage = 25f;
            person.damage = 0f;
            person.projectile_lifetime = 1F/person.size;
            rb = GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            //Spawn(); //comment this out if u don't want the hellspawn -- jeremy what the actual fuck
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
            //Spawn();
            //Compare(); //same reason as Damage() 
            //person.health += (vore_2.kill - vore_1.kill)*25;
            //vore_1.kill = vore_2.kill; //this is a way to have this consistently checked, otherwise it could be a fixed update
            //person.size = Convert.ToSingle(1 / Math.Log(vore_1.kill + 1.5, 2)); //this changes the "size," which is really just the multiplier for speed
            //person.projectile_lifetime = 1F / person.size;
        }
        public static void Degen() //health degeneration
        {
            if (person.health <= 0.0f) //health degeneration / damage might push it below zero
            {
                //Player goes to death screen
                SceneManager.LoadScene(3);
                person.size = 0.0f; //this destroys speed
                person.health = 0.0f; //so it's constantly in the zero loop
                person.damage = 0;
                vore_1.kill = 0;
                vore_2.kill = 0;
            }
            if (vore_2.kill >= 20)
            {
                person.health -= person.size * person.rate; //this is just the degeneration function, called each frame.  Rate is arbitrary
                person.health = Mathf.Round(person.health * 100f) / 100f; //Chucc did this?
            }
            // We can balance these based on kill count, so someone who has 3 kills doesn't experience degeneration or such.  Can be done in void update()
        }

        // Update is called once per frame
        void Update () {
            Damage(); //Called once per frame, but can be realistically be a fixed update
            if (vore_1.kill != vore_2.kill) //Just a work around to change the in-game size
            {
                Debug.Log("v1 " + vore_1.kill);
                Debug.Log("v2 " + vore_2.kill);
                //Upsize();
                transform.localScale += new Vector3(0.1f, 0.1f, 0); //Changes the visible size, including collision boundaries in the form (x,y,z)
            }
            Degen(); //Needs to be called once per frame
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // This takes input in the form of arrow keys and WASD
            SocketIOClient.velocities[name] = moveInput.normalized * speed; //This determines direction of the velocity
            bool v = SocketIOClient.velocities[name] != new Vector2(0, 0) || speed != 0; //To make sure that the speed isn't zero
            if (v && name == SocketIOClient.currentPlayer)
            {
                SocketIOClient.velocities[SocketIOClient.currentPlayer] = SocketIOClient.velocities[SocketIOClient.currentPlayer] * person.size;//Unity Discord said weird things happen with zero speed, they might be wrong
            }
	    }

        void FixedUpdate()//Could put damage, size, etc in here to save input lag
        {
            if (name == SocketIOClient.currentPlayer)
            {
                rb.MovePosition(rb.position + SocketIOClient.velocities[SocketIOClient.currentPlayer] * Time.fixedDeltaTime);//Changes position on map based on velocity 
            }
        }

    }
}
