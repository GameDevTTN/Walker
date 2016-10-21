using UnityEngine;
using System.Collections;

public class usernameField : MonoBehaviour {
	public UnityEngine.UI.InputField username;

	string temp;

	void Start() {
		temp = "";
		if (PlayerPrefs.HasKey("pName")) {
			username.text = PlayerPrefs.GetString ("pName");
		}

	}
	public void onEnd() {
		

		temp = username.text;
	
		if (temp.Equals ("f#167")) {
			//set Dog
			PlayerPrefs.SetString ("pPet", "Dog");
		} else if (temp.Equals ("f#168")) {
			PlayerPrefs.SetString ("pPet", "Snowman");


		} else if (temp.Equals ("f#169")) {
			PlayerPrefs.SetString ("pPet", "Bear");


		} else if (temp.Equals ("f#170")) {
			PlayerPrefs.SetString ("pPet", "Yellow");


		} else if (temp.Equals ("f#all")) {
			PlayerPrefs.SetString ("pPet", "dAll");


		} else if (temp.Equals ("f#reset")) {
			PlayerPrefs.DeleteAll ();

		} else if (temp.Equals ("f#e10")) {
			PlayerPrefs.SetFloat ("eCurrHealth", 4320);
		} else if (temp.Equals ("f#e100")) {
			PlayerPrefs.SetFloat("eCurrHealth", 43200);
		
		}


		else {
			PlayerPrefs.SetString ("pName", temp);
			print (temp + " sd");
			
		}



		if (!PlayerPrefs.HasKey ("pName")) {
			PlayerPrefs.SetString ("pName", "");
			username.text = PlayerPrefs.GetString ("pName");
		} else {
			username.text = PlayerPrefs.GetString ("pName");
		}

		}
}