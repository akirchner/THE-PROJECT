using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetTutorialID : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameProperties.currentTutorial = Int32.Parse(SceneManager.GetActiveScene().name.Substring(9, 1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
