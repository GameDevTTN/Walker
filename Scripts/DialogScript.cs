using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Random = System.Random;

public class DialogScript : GPSAdapter {
	
	bool justStart;
	float welcomeTime;

	private bool tracking = false;

	public override void onTrackStart() {
		tracking = true;
	}

	public override void onTrackStop() {
		tracking = false;
	}


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("eCurrHealth")) {
			justStart = false;
		} else {
			justStart = true;
		}


		welcomeTime = 0;

	
		

	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.GetString ("petOrNoPet").Equals ("pet")) {


			
			welcomeTime += Time.deltaTime;


			if (welcomeTime < 4f) {
				if (!justStart) {
					(this.gameObject.GetComponent<Text> ()).text = "Welcome back!";
					//(this.gameObject.GetComponent<Text> ()).text = PlayerPrefs.GetString("pName");

				} else {
					(this.gameObject.GetComponent<Text> ()).text = "Hi, how are you?";
				}
			} else {

				if (tracking) {
					if (PlayerPrefs.GetFloat ("gSpeed") == 0) {
						getDialogs (15);

					} else if (PlayerPrefs.GetFloat ("gSpeed") < 1.3f) {
						//Handheld.Vibrate (); // code moved in speeed checker
						getDialogs (10); //slow speed
					} else if (PlayerPrefs.GetFloat ("gSpeed") > 10f) {
						getDialogs (11); // high speed
					} else {
						//getDialogs (13); //default time exercised
						getDialogs(2);
					}



				} else {
			
					if (PlayerPrefs.GetFloat ("eCurrHealth") == 0) {
						getDialogs (12); // energy is zero
					} else if (PlayerPrefs.GetFloat ("eCurrHealth") < 4320f) {
						getDialogs (8); //energy is low
					} else if (PlayerPrefs.GetFloat ("eCurrHealth") > 0.9 * 43200) {

						getDialogs (9); // energy is full
					} else {
						getDialogs (14); //want to play

					}

				}

			}
	
		} else {
			if (tracking) {
				if (PlayerPrefs.GetFloat ("gSpeed") == 0) {
					getDialogs (17);

				} else if (PlayerPrefs.GetFloat ("gSpeed") < 1.3f) {
				//	Handheld.Vibrate (); // code moved somewhere else
					getDialogs (16); //slow speed
				} else if (PlayerPrefs.GetFloat ("gSpeed") > 10f) {
					getDialogs (11); // high speed
				} 

			}





		}
	}




	public void getDialogs(int num) {
		string[] dialogs = new string[]{
			"How was your exercise?",
			"How is your day?",
			"Keep going, you can do it!",
			"Just do it!",
			"Keep on striving!",
			"Don't make me dissappointed",
			"Good job!",
			"I'm bored, lets go for a run!",
			"I'm bored, lets go for a walk!",
			"I'm feeling good today!",
			"I need to walk faster!",
			"Are you in a car?",
			"Lets exercise now!",
			"We have exercised for {0}.",
			"Want to play?",
			"Lets go!",
			"You are going too slow",
			"Lets get started"
		};
			

		(this.gameObject.GetComponent<Text> ()).text = dialogs [num].Replace("{0}", ""+(int)PlayerPrefs.GetFloat("gSessionStartTime"));


	}





}


