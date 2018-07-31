using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveTutorial : MonoBehaviour {
    StreamWriter sw;

	// Use this for initialization
	void Start () {
        GameProperties.saveData[0, GameProperties.currentTutorial - 1] = true;

        sw = File.CreateText(Path.Combine(Application.persistentDataPath, "gameData.txt"));

        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 15; j++) {
                sw.WriteLine(GameProperties.saveData[i, j] ? 1 : 0);
            }
        }

        sw.Close();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
