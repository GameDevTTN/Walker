using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GPSTracker : MonoBehaviour {

	public Text debug;
	private LinkedList<LocationInfo> pastLocations = new LinkedList<LocationInfo>();
	private bool tracked = false;
	//public Slider time;
	public float timeInvoked;

	public GameObject achievement;
	public GameObject energy;

	public GameObject[] scriptObjects;

	//private AchievementScript aScript;
	//private playerEnergy eScript;

	private LinkedList<GPSAdapter> listeners = new LinkedList<GPSAdapter>();


	// Use this for initialization
	void Start () {
		//aScript = achievement.GetComponent<AchievementScript> ();
		//eScript = energy.GetComponent<playerEnergy> ();

		foreach (GameObject go in scriptObjects) {
			listeners.AddLast (go.GetComponent<GPSAdapter> ());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleTrack()
	{
		if (tracked)
		{
			stopAndClearTrack();
			tracked = false;
		} else
		{
			track();
			tracked = true;
		}
	}

	void track()
	{
		if (Input.location.isEnabledByUser)
		{
			print("start tracking");
			pastLocations.Clear ();
			Input.location.Start();
			PlayerPrefs.SetFloat("gSessionDistance", 0);
			PlayerPrefs.SetFloat("gSessionStartTime", Time.time);
			foreach (GPSAdapter adapt in listeners) {
				adapt.onTrackStart ();
			}
			InvokeRepeating("periodicTrack", 2f, timeInvoked);
		} else
		{
			debug.text = "ls not enabled";
			debug.text = "" + timeInvoked;
			return;
		}
	}

	void stopAndClearTrack()
	{
		CancelInvoke();
		Input.location.Stop();
		pastLocations.Clear();
		print("stopped tracking");
		foreach (GPSAdapter adapt in listeners) {
			adapt.onTrackStop ();
		}
		debug.text = "Start Tracking";
	}


	public Text textObject;
	void periodicTrack()
	{
		//print("starting periodicTrack");
		LocationServiceStatus status = Input.location.status;
		debug.text = status.ToString();
		//the following code is authored by Unity Technologies (modified for the purpose of debugging
		//source: http://docs.unity3d.com/ScriptReference/LocationService.Start.html

		// Connection has failed
		if (status == LocationServiceStatus.Stopped || status == LocationServiceStatus.Initializing || status == LocationServiceStatus.Failed)
		{
			return;
		}
		else
		{
			// Access granted and location value could be retrieved
			LocationInfo lastData = Input.location.lastData;
			if (lastData.horizontalAccuracy > 20) {  // added this line check for horizontalAccuracy of lastData
				//textObject.text = "Horizontal accuracy " + lastData.horizontalAccuracy;

				return;
			}
			float distance = 0f;
			float recentSpeed = 0f;
			float recentTimeLapse = 1f;
				if (pastLocations.Count == 0) {
					pastLocations.AddLast (lastData);
				} else if (lastData.timestamp != pastLocations.Last.Value.timestamp) {
				distance = calculateDistance (pastLocations.Last.Value.latitude, pastLocations.Last.Value.longitude, lastData.latitude, lastData.longitude);
				recentTimeLapse = (float)(lastData.timestamp - pastLocations.Last.Value.timestamp);
				recentSpeed = (int)(distance * 10 / recentTimeLapse) / 10f;
				if (recentSpeed > 10) {
					distance = 0f;
				}
				if (recentSpeed < 1.3) {
					Handheld.Vibrate ();
				}
				PlayerPrefs.SetFloat ("gSpeed", recentSpeed);
				PlayerPrefs.SetFloat("gTotalDistance", PlayerPrefs.GetFloat("gTotalDistance", 0) + distance);
				PlayerPrefs.SetFloat("gSessionDistance", PlayerPrefs.GetFloat("gSessionDistance", 0) + distance);
				pastLocations.AddLast (lastData);
				foreach (GPSAdapter adapt in listeners) {
					adapt.onTrack (distance);
				}
				//aScript.distanceChanged();
				//eScript.distanceChanged (distance);
				}
			
			float timeLapse = (float)(pastLocations.Count <= 1 ? 1 : pastLocations.Last.Value.timestamp - pastLocations.First.Value.timestamp);

			//print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			string s = "";
			s = "You have travelled " + (int)PlayerPrefs.GetFloat("gSessionDistance", 0) + " metres in the last " + (int)timeLapse/60 + " minutes. Your average speed is " + (int)(PlayerPrefs.GetFloat("gSessionDistance", 0) * 10 / timeLapse)/10f + "m/s. Your current speed is " + (recentSpeed > 10 ? 0 : recentSpeed) + "m/s.";
			textObject.text = s;
		}
	}

	private float calculateDistance(float lat1, float long1, float lat2, float long2) {
		float rLat1 = Mathf.Deg2Rad * lat1;
		float rLat2 = Mathf.Deg2Rad * lat2;
		float rDiffLat = (lat2 - lat1) * Mathf.Deg2Rad;
		float rDiffLong = (long2 - long1) * Mathf.Deg2Rad;
		float a = Mathf.Sin (rDiffLat / 2) * Mathf.Sin (rDiffLat / 2) + Mathf.Cos (rLat1) * Mathf.Cos (rLat2) * Mathf.Sin (rDiffLong / 2) * Mathf.Sin (rDiffLong / 2);
		float c = 2 * Mathf.Atan2 (Mathf.Sqrt (a), Mathf.Sqrt (1 - a));

		return 6371 * 1000 * c;
	}
}
