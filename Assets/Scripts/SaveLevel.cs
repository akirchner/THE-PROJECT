using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLevel : MonoBehaviour {
    GameObject[] allObjects;
    StreamWriter sr;
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
        sr = File.CreateText(filename);
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in allObjects) {
            if (gameObject.transform.name == Graviton.name) {
                id = 1;
            }
            else {
                id = 0;
                Console.WriteLine("Whoops, something went wrong in SaveLevel.cs. The object type did not correspond with any preset options.");
            }
            if (id != 0) {
                sr.WriteLine(id);
                sr.WriteLine(gameObject.transform.position.x);
                sr.WriteLine(gameObject.transform.position.y);
                sr.WriteLine(gameObject.transform.eulerAngles.z);
            }
            
        }
        sr.Close();
    }
}
