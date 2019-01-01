using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveUserLevel : MonoBehaviour {

    void save(int levelNum) {
        if (levelNum > 15 || levelNum < 1) {
            Debug.Log("Whoops! Something went wrong in SaveUserLevel.cs");
        }
        else {
            try {
                File.Move(Path.Combine(Application.persistentDataPath, "Edit01.txt"), Path.Combine(Application.persistentDataPath, "User" + levelNum.ToString().PadLeft(2, '0') + ".txt"));
            }
            catch (Exception) {
                Debug.Log("uh oh. the file didn't transfer.");
            }
        }
    }

	// Use this for initialization
	void Start () {

    }

	// Update is called once per frame
	void Update () {
		
	}
}
