using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLevel : MonoBehaviour {
    GameObject[] allObjects;
    StreamWriter sw;
    public Transform Graviton;
    int id;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("end")) {
            Save("newLevel.txt");
        }
	}

    void Save (string filename) {
        if (!File.Exists(filename)) {
            File.Create(filename);
        }
        sw = File.CreateText(filename);
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in allObjects) {

            switch (gameObject.tag) {
            case "graviton":
                id = 1;
                break;
            default:
                id = 0;
                Console.WriteLine("Whoops, something went wrong in SaveLevel.cs. The object type did not correspond with any preset options.");
                break;
            }

            if (id != 0) {
                sw.WriteLine(id);
                sw.WriteLine(gameObject.transform.position.x);
                sw.WriteLine(gameObject.transform.position.y);
                sw.WriteLine(gameObject.transform.eulerAngles.z);
            }
            
        }
        sw.Close();
    }
}
