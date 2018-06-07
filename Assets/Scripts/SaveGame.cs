using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGame : MonoBehaviour {
    StreamWriter sw;
    int packID, levelID;

    // Use this for initialization
    void Start() {
        switch(GameProperties.levelFilename.Substring(0, 4)) {
        case "Grav":
            packID = 0;
            break;
        case "Elec":
            packID = 1;
            break;
        case "Flux":
            packID = 2;
            break;
        default:
            break;
        }

        GameProperties.saveData[packID, Int32.Parse(GameProperties.levelFilename.Substring(4, 2)) - 1] = true;

        sw = File.CreateText(Path.Combine(Application.persistentDataPath, "gameData.txt"));

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 15; j++) {
                Debug.Log(GameProperties.saveData[i,j]);
                sw.WriteLine(GameProperties.saveData[i,j] ? 1 : 0);
            }
        }

        sw.Close();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
