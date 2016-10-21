using UnityEngine;
using System.Collections;
using System;
public class alarmNotification : MonoBehaviour {


	public UnityEngine.UI.InputField hoursInput;
	public UnityEngine.UI.InputField minutesInput;
	public UnityEngine.UI.InputField amInput;
	public UnityEngine.UI.Text confirmation;

	string hour;
	string minute;
	string second;
	string am;

	int minuteInt;
	int hourInt;
	int secondInt;

	int hoursInputInt;
	int minutesInputInt;

	int totalInput;
	int totalTime;

	int notifTime;

	bool clicked;
	void Start () {
		clicked = false;
	}
		



	public void onBast() {
		clicked = true;

		try {
			if(PlayerPrefs.HasKey("aClicked")) { //delete previous notification that was set
				PlayerPrefs.DeleteKey("aClicked");
				LocalNotification.CancelNotification(1);
			}


		hour = System.DateTime.Now.Hour.ToString();
		minute = System.DateTime.Now.Minute.ToString ();
		second = System.DateTime.Now.Second.ToString ();


		hourInt = Int32.Parse (hour);
		minuteInt = Int32.Parse (minute);
		secondInt = Int32.Parse (second);

		hoursInputInt = Int32.Parse (hoursInput.text);
		minutesInputInt = Int32.Parse (minutesInput.text);
		if (amInput.text == "pm") {
			hoursInputInt += 12;
		}

		if (hoursInputInt == 12 && amInput.text == "am") {
			hoursInputInt -= 12;
		}
			if(hoursInputInt < hourInt) {


			}

		totalInput = ((hoursInputInt * 60) + minutesInputInt) * 60;
		totalTime = (((hourInt * 60) + minuteInt) * 60) + secondInt;

		notifTime = totalInput - totalTime;

		print ("Total input: " + totalInput);
		print ("Total Time: " + totalTime);
		print ("total notif time: " + notifTime);



			PlayerPrefs.SetString("aClicked","true");
		LocalNotification.SendNotification(1, notifTime, PlayerPrefs.GetString("pName"), "Take me on a walk please", new Color32(0xff, 0x44, 0x44, 255));

		confirmation.text = "Alarm is set";

		} catch (Exception e) {
			print (e);
			
		}

	}




}
