using UnityEngine;
using System.Collections;

public interface GPSListener {

	void onTrackStart();

	void onTrackStop();

	void onTrack(float distance);
}
