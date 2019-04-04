using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector2 target;
    private Vector3 moveDirection;
    public float speed;
    static public double damage;
    public float lifeTime;
    //public float size;

    private void Start()
    {
        //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);//Seb please explain
        moveDirection.z = 0;
        moveDirection.Normalize();
        lifeTime = 1.0F;
        //transform.localScale += new Vector3(size, size, 0);
        //Player.person.projectile_lifetime = 1.0F;
    }

    private void Update()
    {
        //size = Player.vore_2.kill / 4;
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        transform.position = transform.position + moveDirection * speed * Time.deltaTime * Player.person.size; //This changes speed based on player size, so larger players = slower bullets

        //if(Vector2.Distance(transform.position, target) < 0.2f)
        //{
        //    Destroy(gameObject);
        //}

        //lifeTime is subtracted from with time, and once it hits less than zero it explodes
        lifeTime -= Time.deltaTime;
        
        if (lifeTime < 0.0)
        {
            Destroy(gameObject);
        }


        if (Player.vore_1.kill != Player.vore_2.kill)//Same reasoning in the Player.cs file
        {
            transform.localScale += new Vector3(1.0f, 1.0f, 0);
        }
        if (Player.person.health <= 0)//This prevents new projectiles from spawning after death, because previously they would stay on screen and potentially lag
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