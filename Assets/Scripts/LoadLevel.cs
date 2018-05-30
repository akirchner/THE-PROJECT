using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
    private List<List<int>> levelData;
    private List<int> currentObjectData;
    private string line;
    private int itemID;
    public Transform levelObject;
    public Transform graviton;

	// Use this for initialization
	void Start () {
        try {
            levelData = new List<List<int>>();
            StreamReader sr = new StreamReader(GameProperties.levelFilename);
            line = sr.ReadLine();
            itemID = 0;
            while(line != null) {
                currentObjectData = new List<int>();

                currentObjectData.Add(Int32.Parse(line));
                currentObjectData.Add(Int32.Parse(sr.ReadLine()));
                currentObjectData.Add(Int32.Parse(sr.ReadLine()));
                currentObjectData.Add(Int32.Parse(sr.ReadLine()));

                levelData.Add(currentObjectData);

                line = sr.ReadLine();

                itemID++;
            }

            sr.Close();

            for (int i = 0; i < levelData.Count; i++) {
                switch (levelData[i][0]) {
                case 1:
                    levelObject = graviton;
                    break;
                default:
                    Console.WriteLine("Whoops, something went wrong in LoadLevel.cs. The object ID did not correspond with any preset values.");
                    break;
                }

                Instantiate(levelObject, new Vector3(levelData[i][1], levelData[i][2]), Quaternion.Euler(0, 0, levelData[i][3])); 
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
