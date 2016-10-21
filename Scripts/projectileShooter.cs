﻿using UnityEngine;
using System.Collections;

public class projectileShooter : MonoBehaviour {


	GameObject prefab;
	// Use this for initialization
	void Start () {
		prefab = Resources.Load ("projectile") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			
			GameObject projectile = Instantiate (prefab) as GameObject;
		//	projectile.transform.position = transform.position + Camera.main.transform.forward * 100 ;
			Rigidbody rb = projectile.GetComponent<Rigidbody> ();
			rb.velocity = Camera.main.transform.forward * 40;
			print (projectile.transform.position.z);

			//keep it for later I guess?



		}


	}
}
