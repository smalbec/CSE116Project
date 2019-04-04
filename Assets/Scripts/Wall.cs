using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float health = 150; //Could just create a new instance of Traits

    private void Start() //Put health in here so now I think it's only a thing for a single wall class
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Projectile"))
        {
            health = health - Player.person.damage;//Same reasoning as PlayerDummy.cs for this entire block
            if (health <= 0)
            {
                health = 150;
                Destroy(gameObject);
                //only difference is that destroying a wall doesn't increase kill count for obvious reasons
            }

        }
    }
}
