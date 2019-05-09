using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wig { 
    public class DeadPlayer : MonoBehaviour
    {
        public double health; //Could just create a new instance of Traits
        private Transform playerPos;
        public int kill;
        public static float lifeTime;
        public bool playerContact;//chucc big gay 

        private void Start() //Put health in here so now I think it's only a thing for a single wall class
        {
            kill = 1;
            health = 100;
            playerPos = GetComponent<Transform>();
            lifeTime = 2.0F;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Pwn'd");
            if (col.CompareTag("Projectile"))//Checks to see what's colliding with it
            {
                health = health - wig.Player.person.damage;//Subtracts based on the Player's damage
                if (health <= 0)
                {
                    Destroy(gameObject);//Destroys dummy
                    if (CompareTag("Player"))//this didn't work :<
                    {
                        lifeTime -= Time.deltaTime;
                        if (lifeTime <= 0)
                        {
                            Destroy(gameObject);
                        }
                    }
                }
            }

            if (col.gameObject.tag == "Player")
            {
                playerContact = true;
            }
        }

        void OnCollisionExit(Collision col)
        {
            if (col.gameObject.tag == "Player")
            {
                playerContact = false;
            }
        }

        private void Update()
        {
            if (playerContact)
            {
                SocketIOClient.eggHP[name] -= Time.deltaTime;
            }
            Debug.Log(name);
            if (SocketIOClient.eggHP[name] < 0.0F)
            {
                //wig.Player.vore_2.kill += kill;//Increases player kill count
                Destroy(gameObject);
                wig.SocketIOClient.eggCheck[name] = false;
                Debug.Log("player ded");
                wig.SocketIOClient.eggState = true;
            }
        }
    }
}