using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResetProgress : MonoBehaviour {
    StreamWriter sw;

    // Use this for initialization
    void Start () {
        sw = File.CreateText(Path.Combine(Application.persistentDataPath, "gameData.txt"));

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 15; j++) {
                GameProperties.saveData[i, j] = false;
                sw.WriteLine(0);
            }
        }

        sw.Close();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
