using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public GameObject shot;
    private Transform playerPos;
    private bool isCoolDown;
    private float coolDownTimer;
    
    //This was all Seb so he can explain


	// Use this for initialization
	void Start () {
        playerPos = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        //once a player shoots, cooldown begins. this means that while coolDownTimer is less than 0, the player cannot shoot
        if (isCoolDown)
        {
            if (coolDownTimer > 0.0F)
            {
                coolDownTimer -= Time.deltaTime;
            }
            else
            {
                isCoolDown = false;
            }
        }



        if (Input.GetMouseButtonDown(0) && isCoolDown == false)
        {
            Instantiate(shot, playerPos.position, Quaternion.identity);
            isCoolDown = true;
            coolDownTimer = .25F;
        }
	}

}
