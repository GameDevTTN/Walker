using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class mainUI : MonoBehaviour {

	public void ChangeScene(string sceneName) {
		SceneManager.LoadScene (sceneName);
	}




}
