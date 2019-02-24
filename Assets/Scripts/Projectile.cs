using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector2 target;
    private Vector3 moveDirection;
    public float speed;
    


    private void Start()
    {
        //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
    }

    private void Update()
    {

        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        transform.position = transform.position + moveDirection * speed * Time.deltaTime;

        //if(Vector2.Distance(transform.position, target) < 0.2f)
        //{
        //    Destroy(gameObject);
        //}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerDummy")
        {
            Debug.Log("collides with player dummy");
            Destroy(col.gameObject);
            Destroy(gameObject);
            


        }
    }

}