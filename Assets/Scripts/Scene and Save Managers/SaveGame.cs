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
            packID = 1;
            break;
        case "Elec":
            packID = 2;
            break;
        case "Flux":
            packID = 3;
            break;
        case "Mixed":
            packID = 4;
            break;
        default:
            break;
        }

        GameProperties.saveData[packID, Int32.Parse(GameProperties.levelFilename.Substring(4, 2)) - 1] = true;

        sw = File.CreateText(Path.Combine(Application.persistentDataPath, "gameData.txt"));

        for (int i = 0; i < GameProperties.saveData.GetLength(0) - 1; i++) {
            for (int j = 0; j < 15; j++) {
                sw.WriteLine(GameProperties.saveData[i,j] ? 1 : 0);
            }
        }

        sw.Close();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
