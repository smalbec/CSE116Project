using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDummy : MonoBehaviour
{
    public double health; //Could just create a new instance of Traits
    private Transform playerPos;
    public GameObject body;
    public int kill;
    public float lifeTime;

    private void Start() //Put health in here so now I think it's only a thing for a single wall class
    {
        kill = 1;
        health = 100;
        playerPos = GetComponent<Transform>();
        lifeTime = 1.0F;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Projectile"))//Checks to see what's colliding with it
        {
            health = health - Player.person.damage;//Subtracts based on the Player's damage
            if (health <= 0)
            {
                //health = 100;//Resets for all dummies, they share a health pool (we need to fix that)
                Destroy(gameObject);//Destroys dummy
                //Player.vore_2.kill += kill;//Increases player kill count
                //Player.person.health += 25;
                Instantiate(body, playerPos.position, Quaternion.identity);
                kill = 0;
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
    }
}