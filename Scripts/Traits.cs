using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traits : MonoBehaviour
{
    public float size;
    public float rate;
    public float health;
    public float base_damage;
    public float damage;
    public float projectile_lifetime;//This is for Colin's idea of only letting the projectiles last for so long in the air, should be a function of size.
    public float respawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("wig");
        Instantiate(Resources.Load("Player") as GameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
