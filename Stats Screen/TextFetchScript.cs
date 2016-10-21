using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFetchScript : MonoBehaviour {

	public string key;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		(this.gameObject.GetComponent<Text>()).text = "" + (int)PlayerPrefs.GetFloat (key, 0) + "m";
	}
}
