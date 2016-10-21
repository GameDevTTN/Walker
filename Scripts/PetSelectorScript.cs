using UnityEngine;
using System.Collections;

public class PetSelectorScript : MonoBehaviour {

	public GameObject[] pets;
	public GameObject ball;
	public GameObject energy;

	// Use this for initialization
	void Start () {
		foreach (GameObject go in pets) {
			go.SetActive(false);
		}
		ball.SetActive (false);
		energy.SetActive (false);
		if (PlayerPrefs.HasKey("pPet")) {
			switch (PlayerPrefs.GetString("pPet")) {
			case "Empty":
				//the rest of it
				break;
			case "Dog":
				pets [0].SetActive (true);
				ball.SetActive (true);
				energy.SetActive (true);
				PlayerPrefs.SetString ("petOrNoPet", "pet");
				break;
			case "Snowman":
				pets[1].SetActive(true);
				ball.SetActive (true);
				energy.SetActive (true);
				PlayerPrefs.SetString ("petOrNoPet", "pet");

				break;
			case "Bear":
				pets[2].SetActive(true);
				ball.SetActive (true);
				energy.SetActive (true);
				PlayerPrefs.SetString ("petOrNoPet", "pet");

			break;
			case "Yellow":
				pets[3].SetActive(true);
				ball.SetActive (true);
				energy.SetActive (true);
				PlayerPrefs.SetString ("petOrNoPet", "pet");

				break;
			case "dAll":
				break;

				
			default:
				

				break;
			}
		} else {
			//turn everything falase;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
