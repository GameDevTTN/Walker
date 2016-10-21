using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class throwBall : MonoBehaviour {
	//public GameObject dog;
	//private DogWalk sDogWalk;
	bool clicked;
	float throwTime;
	float timePause;
	bool intersect;
	Animator anim;
	void Start() {
		clicked = false;
		intersect = false;
		 anim = gameObject.GetComponent<Animator> ();


	}

	void FixedUpdate()  {



		//if (Input.GetMouseButtonDown (0) && PlayerPrefs.GetFloat ("eCurrHealth") > 0) {  // to test throwing the ball in pc
		if (!clicked && PlayerPrefs.GetFloat ("eCurrHealth") > 0) {   // to throw the ball in mobile phone without getting the top and botton portion; when clicked throw the ball as well (E.g. Pressing the back button making throwing the ball) 
				foreach (Touch touch in Input.touches) {
					if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled && touch.position.y < Screen.height * 0.75 && touch.position.y > 0.1 *Screen.height ) {
			clicked = true;
			anim.enabled = true;
		}
			}
		}	
		else if (PlayerPrefs.GetFloat ("eCurrHealth") == 0) {
			anim.enabled = false;


		}


		if (clicked) {

		
			throwTime += Time.deltaTime;
			if (throwTime < 5) {
				if (intersect == false) {
					transform.position = new Vector3 ((int)(320 + 42.5 * throwTime), (int)(-218.75 * throwTime * throwTime + 525 * throwTime + 940), (int)(800 * throwTime - 3999));
				}
				if (transform.position.x == 427) { 
					
					intersect = true;
				}
				if (intersect) {
					transform.position = new Vector3 (transform.position.x, transform.position.y-1.6f, transform.position.z - 3.7f);

				}
			} else {
				
				if (timePause < 1) {
					timePause += Time.deltaTime;
				} else {
					
					clicked = false;
					intersect = false;
				}
			}





		} else {
			transform.position = new Vector3 (360, 930, -5000);
			throwTime = 0f;
			timePause = 0;

		}




	}



}
