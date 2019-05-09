using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Projectile : MonoBehaviour {

    private Vector2 target;
    private Vector3 moveDirection;
    public float speed;
    static public double damage;
    public float lifeTime = wig.Player.person.projectile_lifetime;
    

    private void Start()
    {
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position); // Position to where the projectile is being shot (the mouse), mantains direction when normalized
        moveDirection.z = 0;
        moveDirection.Normalize();
        lifeTime = wig.Player.person.projectile_lifetime;
        transform.localScale = new Vector3(Convert.ToSingle(0.125*(wig.Player.vore_2.kill+1)), Convert.ToSingle(0.125 * (wig.Player.vore_2.kill+1)), 0);
        
    }

    private void Update()
    {
        

        transform.position = transform.position + moveDirection * speed * Time.deltaTime * wig.Player.person.size; //This changes speed based on player size, so larger players = slower bullets

        //we need to add collision tag for other players
        //lifeTime is subtracted from with time, and once it hits less than zero it explodes
        lifeTime -= Time.deltaTime;
        
        if (lifeTime < 0.0)
        {
            Destroy(gameObject);
        }


        if (wig.Player.vore_1.kill != wig.Player.vore_2.kill)//Same reasoning in the Player.cs file
        {
            transform.localScale += new Vector3(1.0f, 1.0f, 0);
        }
        if (wig.Player.person.health <= 0)//This prevents new projectiles from spawning after death, because previously they would stay on screen and potentially lag
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerDummy")//Checks to see what it's colliding with, in this case red boi
        {
            Destroy(gameObject);//Destroys the projectile
        }

        if(col.gameObject.tag == "Obstacle")//This is wall
        {
            Destroy(gameObject);//Destroys the projectile
        }

        if(col.gameObject.tag == "DeadPlayer")
        {
            Debug.Log("epic win");
            Destroy(gameObject);
        }
    }



}