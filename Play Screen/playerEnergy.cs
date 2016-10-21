using UnityEngine;
using System.Collections;
using System;


public class playerEnergy : GPSAdapter {

	public float maxHealth;
	float currHealth;
	public GameObject bar;
	bool healthZero;
	int currTime;
	int prevTime;
	int diffTime;


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("eCurrHealth")) {               //load previous health before game closes
			
			prevTime = PlayerPrefs.GetInt("eTimeStamp");
			currTime = unixTime ();

			diffTime = currTime - prevTime;

			currHealth = PlayerPrefs.GetFloat ("eCurrHealth");
			currHealth = currHealth - (float)diffTime;

			if (currHealth < 0) {
				currHealth = 0;
			}
			PlayerPrefs.SetFloat ("eCurrHealth", currHealth);
			float calEnergy =  currHealth / maxHealth;
			setEnergyBar (calEnergy);
		} else { 												//Game is never opened before (new game) 
			currHealth = maxHealth;
		}

		healthZero = false; 
		InvokeRepeating ("decreaseHealth", 1, 1);
	}

	void decreaseHealth() {

		lock (this) {
			if (currHealth > 0) {
				
				if (PlayerPrefs.HasKey ("ePlayTime")) {
					
					if (PlayerPrefs.GetFloat ("ePlayTime") < 4.8f) {  // run 5 times 

						currHealth -= (0.0020f * maxHealth) - 1f;  // decrease 1% everytime play with pet

					
					}
				} else {
					currHealth -= 1; // decrease health by 1 every second 

				}

				if (currHealth < 0) {
					currHealth = 0;
				}

				PlayerPrefs.SetFloat ("eCurrHealth", currHealth);
				PlayerPrefs.SetInt ("eTimeStamp", unixTime());
		

				float calEnergy = (float)currHealth / maxHealth;
				setEnergyBar (calEnergy);
			} else {
				healthZero = true;
			} 
			if (healthZero) {
			

				healthZero = false;
			}
		}
	}

	private void setEnergyBar(float myEnergy) {
		bar.transform.localScale = new Vector3 (myEnergy, bar.transform.localScale.y,bar.transform.localScale.z);
	}


	//Get Unix time
	public int unixTime() {
		var time = DateTime.Now - new DateTime(1970,1,1,0,0,0);
		return (int)time.TotalSeconds;
	}



	public override void onTrack(float distance) {
		lock (this) {
			
			currHealth += distance / 2500f * maxHealth; // based on 1.4m/s for 30 minutes (brisk walking)
			if (currHealth > maxHealth) {
				currHealth = maxHealth;
			} 
				


		}
	}


}
