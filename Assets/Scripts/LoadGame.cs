﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadGame : MonoBehaviour {
    string filepath;

	// Use this for initialization
	void Start () {
        filepath = Path.Combine(Application.persistentDataPath, "gameData.txt");

        if (File.Exists(filepath)) {
            StreamReader sr = new StreamReader(filepath);

            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 15; j++) {
                    GameProperties.saveData[i,j] = sr.ReadLine().Equals("1");
                }
            }

            sr.Close();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}