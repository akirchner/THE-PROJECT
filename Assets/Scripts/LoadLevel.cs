using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
    private List<List<double>> levelData;
    private List<double> currentObjectData;
    private string line;
    public Transform levelObject;
    public Transform graviton;

	// Use this for initialization
	void Start () {
        try {
            levelData = new List<List<double>>();
            StreamReader sr = new StreamReader(GameProperties.levelFilename);
            line = sr.ReadLine();
            while(line != null) {
                currentObjectData = new List<double>();

                currentObjectData.Add(Double.Parse(line));
                currentObjectData.Add(Double.Parse(sr.ReadLine()));
                currentObjectData.Add(Double.Parse(sr.ReadLine()));
                currentObjectData.Add(Double.Parse(sr.ReadLine()));

                levelData.Add(currentObjectData);

                line = sr.ReadLine();
            }

            sr.Close();

            for (int i = 0; i < levelData.Count; i++) {
                switch ((int) levelData[i][0]) {
                case 1:
                    levelObject = graviton;
                    break;
                default:
                    Console.WriteLine("Whoops, something went wrong in LoadLevel.cs. The object ID did not correspond with any preset values.");
                    break;
                }

                Instantiate(levelObject, new Vector3((float) levelData[i][1], (float) levelData[i][2]), Quaternion.Euler(0, 0, (float) levelData[i][3])); 
            }
        }
        catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
