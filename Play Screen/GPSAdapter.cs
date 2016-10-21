using UnityEngine;
using System.Collections;

public class GPSAdapter : MonoBehaviour, GPSListener {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void onTrackStart() {
	}
	public virtual void onTrackStop() {
	}
	public virtual void onTrack(float distance) {
	}
		
}
