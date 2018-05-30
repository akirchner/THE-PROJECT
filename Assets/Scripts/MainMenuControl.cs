using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

	public void LoadScene (string level){

		SceneManager.LoadScene (level);

	}

	public void SelectLevel (string filePath){

		GameProperties.levelFilename = filePath;
		Debug.Log (GameProperties.levelFilename);
		SceneManager.LoadScene ("Level");

	}
}
