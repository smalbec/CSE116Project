using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float health; //Could just create a new instance of Traits
    public GameObject wall;
    private Transform wallPos;
    public int respawnTime;

    private void Start() //Put health in here so now I think it's only a thing for a single wall class
    {
        wallPos = GetComponent<Transform>();
        var all = wall;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Projectile"))
        {
            health = health - wig.Player.person.damage;//Same reasoning as PlayerDummy.cs for this entire block
            if (health <= 0)
            {
                health = 150;
                Invoke("Respawn", respawnTime);// starts timer to respawn in 15s

                gameObject.SetActive(false);
                //only difference is that destroying a wall doesn't increase kill count for obvious reasons
            }

        }
    }

    void Respawn()
    {
        var obj = Instantiate(wall, wallPos.position, wallPos.rotation);
        obj.SetActive(true);
    }



}
