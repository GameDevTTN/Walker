using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class DogWalk : MonoBehaviour {
	
	bool clicked;
	bool intersect;
	float dogTime;
	float timePause;
	public Vector3 defaultPosition;
	Animator anim;

	// Use this for initialization
	public void Start () {
		//(this.gameObject.GetComponent<Transform>()).transform.position = new Vector3 (1,1,1);
		clicked = false;
		intersect = false;
		anim= gameObject.GetComponent<Animator> ();


	}


	void FixedUpdate () {

		//if (Input.GetMouseButtonDown (0) && PlayerPrefs.GetFloat ("eCurrHealth") > 0) {
		if (!clicked && PlayerPrefs.GetFloat ("eCurrHealth") > 0) {
			foreach (Touch touch in Input.touches) {
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled && touch.position.y < Screen.height * 0.75 && touch.position.y > 0.1 * Screen.height) {
					clicked = true;
				anim.enabled = true;
				}

			}
		} else if (PlayerPrefs.GetFloat ("eCurrHealth") == 0) {
			anim.enabled = false;
			clicked = false;

		}

		if (clicked) {
			//print (dogTime);
			dogTime += Time.deltaTime;
		//	print (dogTime + " after");

			if (dogTime < 5) {
				PlayerPrefs.SetFloat ("ePlayTime", dogTime);
				if (intersect == false) {
					transform.position = new Vector3 (transform.position.x, transform.position.y - 1.5f, transform.position.z - 9.8f);
				}
			//	if (transform.position.y == 827) { 
			//		intersect = true;

					//Do like the dog
			//	}
				/*
				if (intersect) {
					
					transform.position = new Vector3 (transform.position.x, transform.position.y - 2, transform.position.z - 16);

				} */
			

			} else {
				PlayerPrefs.DeleteKey ("ePlayTime");
				if (timePause < 1) {
					timePause += Time.deltaTime;
				} else {
					clicked = false;
					intersect = false;
				}
			}
		} else {
			transform.position = defaultPosition;// new Vector3 (460,990,0);
			dogTime = 0f;
			timePause = 0;

		}




	} 




}
