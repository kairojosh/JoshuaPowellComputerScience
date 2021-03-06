﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {
    //References
    public LayerMask playerMask;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
    public GameObject target;
	//Handling
	public float Recoil = 1f;
	public float ReloadSpeed = 1f;
	public float bulletVelocity = 6f;
	public float bulletDuration = 2f;
	public float velocity = 0.2f;
    public float rotationSpeed = 1000f;
    public float stopDistance = 1f;

    public Transform player;
    public GridScript pathReference;
    //Local Variables
    private Quaternion targetRotation; 
	private float TimeStamp;
	private float speed = 5f;
    private CharacterController controller;



     void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        TimeStamp = Time.time;

    }

    void Fire(){
		var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody> ().velocity = bulletSpawn.transform.forward * bulletVelocity;

		Destroy (bullet, bulletDuration);
		//When it's time to start firing again
		TimeStamp = Time.time + ReloadSpeed;
	}

		
	// Update is called once per frame
	void Update () {
        //Firing sort of works, but breaks occasionally, needs fixing
    
        if(Time.time >= TimeStamp ){
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, 10, playerMask))
            {
                Fire();
                
            }
          

        }
   

        if (pathReference.path != null)
        {
            for (int i = 0; i < pathReference.path.Count - 1; i++)
            {
                Vector3 difference = player.position - gameObject.transform.position;
                difference.Normalize();
                targetRotation = Quaternion.LookRotation(difference);
                transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

                Vector3 nodeAIdifference= (pathReference.path[i].position - gameObject.transform.position).normalized;
                controller.Move(nodeAIdifference * Time.deltaTime * velocity);

         

            }
        }
    
     
      /* implement this in college
        //Determines if the player is moving backwards, sideways or forwards
        if (Vector3.Dot(transform.forward, motion) > .4)
		{
			speed = ForwardsVelocity;
		}
		else if(Vector3.Dot(transform.forward, motion) < -.6)
		{
			speed = BackwardsVelocity;
		}
		else
		{
			speed = SidewaysVelocity;
		}
        */

	}
}
