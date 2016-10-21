using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementFetchScript : MonoBehaviour {

	public AchievementScript aScript;
	public int index;
	public Text goal;
	public Text nextGoal;
	public Text achievedGoal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		AchievementScript.Achievement a = aScript.achievements[index];
		int level = a.level + 1;
		(this.gameObject.GetComponent<Text> ()).text = level + "";
		//goal.text = a.nextGoal();
		//nextGoal.text = 
		goal.text = a.name;
		nextGoal.text = a.nextGoal ();
		if (index == 0) {
			achievedGoal.text = "Percentage completed " +  (int)((PlayerPrefs.GetFloat ("gTotalDistance") / a.currentGoal ()) * 100) + "%";
		} else {
			achievedGoal.text = "Percentage completed " + (int)((PlayerPrefs.GetFloat ("gSessionDistance") / a.currentGoal ()) * 100) + "%";
		}
		//nextGoal and name are also text field that is useable
	}
}
