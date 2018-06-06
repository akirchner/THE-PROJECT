using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveLevel : MonoBehaviour {
    GameObject[] allObjects;
    StreamWriter sw;
    int id;
    List<double> extraData;
    List<bool> beamList;
    StringBuilder beamProperties;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("end")) {
            Save("newLevel.txt");
        }
	}

    private int parseForceType(ForceType type)
    {
        switch(type) {
        case ForceType.Graviton:
            return 1;
        case ForceType.Fluxion:
            return 2;
        case ForceType.Electron:
            return 3;
        default:
            return 0;
        }
    }

    void Save (string filename) {
        sw = File.CreateText(Path.Combine(Application.streamingAssetsPath, filename));

        sw.WriteLine(GameObject.Find("GravitonButton").GetComponent<newParticle>().numAvailable);
        sw.WriteLine(GameObject.Find("ElectronButton").GetComponent<newParticle>().numAvailable);
        sw.WriteLine(GameObject.Find("FluxionButton").GetComponent<newParticle>().numAvailable);

        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in allObjects) {
            extraData = new List<double>();

            switch (gameObject.tag) {
            case "Beam":
                id = 0;
                beamProperties = new StringBuilder();
                beamList = gameObject.GetComponent<Beam>().getProperties();
                foreach (bool i in beamList) {
                    beamProperties.Append(i ? 1 : 2);
                }
                extraData.Add(Int32.Parse(beamProperties.ToString()));
                extraData.Add(0);
                break;
            case "Goal":
                id = 1;
                extraData.Add(gameObject.GetComponent<Finalization>().targetCount);
                extraData.Add(gameObject.transform.localScale.x);
                break;
            case "Wall":
                id = 2;
                extraData.Add(gameObject.transform.localScale.x);
                extraData.Add(gameObject.transform.localScale.y);
                break;
            case "DragableForce":
                id = 3;
                extraData.Add(parseForceType(gameObject.GetComponent<Properties>().getType()));
                extraData.Add(gameObject.GetComponent<Properties>().size);
                break;
            case "DynamicForce":
                id = 4;
                extraData.Add(parseForceType(gameObject.GetComponent<Properties>().getType()));
                extraData.Add(gameObject.GetComponent<Properties>().size);
                break;
            case "StaticForce":
                id = 5;
                extraData.Add(parseForceType(gameObject.GetComponent<Properties>().getType()));
                extraData.Add(gameObject.GetComponent<Properties>().size);
                break;
            case "Mirror":
                id = 6;
				extraData.Add(gameObject.transform.localScale.x);
				extraData.Add(gameObject.transform.localScale.y);
                break;
            default:
                id = -1;
                Console.WriteLine("Whoops, something went wrong in SaveLevel.cs. The object type did not correspond with any preset options.");
                break;
            }

            if (id >= 0) {
                sw.WriteLine(id);
                sw.WriteLine(gameObject.transform.position.x);
                sw.WriteLine(gameObject.transform.position.y);
                sw.WriteLine(gameObject.transform.eulerAngles.z);
                foreach (double i in extraData) {
                    sw.WriteLine(i);
                }
            }
            
        }
        sw.Close();
    }
}
