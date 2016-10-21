using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class petName : MonoBehaviour {

	// Use this for initialization
	void Start () {
		(this.gameObject.GetComponent<Text> ()).text = PlayerPrefs.GetString("pName");

	}

	// Update is called once per frame
	void Update () {
	
	}
}
