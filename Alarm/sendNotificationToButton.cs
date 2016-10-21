using UnityEngine;
using System.Collections;

public class sendNotificationToButton : MonoBehaviour {

	public void onClick() {


		LocalNotification.SendNotification(1, 600, "Snowman", "Take me on a ride with you", new Color32(0xff, 0x44, 0x44, 255));
		print ("a");

	}
}



