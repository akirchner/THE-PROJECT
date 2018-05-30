using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
    private int[][] levelData;
    private string line;
    private int itemID;
    public Transform levelObject;
    public Transform graviton;

	// Use this for initialization
	void Start () {
        try {
            StreamReader sr = new StreamReader(GameProperties.levelFilename);
            line = sr.ReadLine();
            itemID = 0;
            while(line != null) {
                levelData[itemID][0] = Int32.Parse(line);
                levelData[itemID][1] = Int32.Parse(sr.ReadLine());
                levelData[itemID][1] = Int32.Parse(sr.ReadLine());
                levelData[itemID][1] = Int32.Parse(sr.ReadLine());
                line = sr.ReadLine();

                itemID++;
            }

            sr.Close();

            for (int i = 0; i < levelData.Length; i++) {
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
