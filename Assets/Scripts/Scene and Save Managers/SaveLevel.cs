using System;
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
	public GameObject theBeam;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("`")) {
            Debug.Log("Saving!");
            save("znewLevel.txt");
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

    private int parseDynamicReaction(ReactType type) {
        switch(type) {
        case ReactType.Gravity:
            return 1;
        case ReactType.Flux:
            return 2;
        case ReactType.Positive:
            return 3;
        case ReactType.Negative:
            return 4;
        default:
            return 0;
        }
    }

    public void save (string filename) {
        if (filename.Substring(0, 4).Equals("User") || filename.Substring(0, 4).Equals("Edit")) {
            sw = File.CreateText(Path.Combine(Application.persistentDataPath, filename));
        }
        else {
            sw = File.CreateText(Path.Combine(Application.streamingAssetsPath, filename));
        }


            sw.WriteLine(GameObject.Find("GravitonButton").GetComponent<ForceSpawner>().numAvailable);
            sw.WriteLine(GameObject.Find("ElectronButton").GetComponent<ForceSpawner>().numAvailable);
            sw.WriteLine(GameObject.Find("FluxionButton").GetComponent<ForceSpawner>().numAvailable);

            allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach(GameObject gameObj in allObjects)
            {
                extraData = new List<double>();

                switch(gameObj.tag)
                {
                    case "Beam":
                        id = 0;
                        beamProperties = new StringBuilder();
                        beamList = gameObj.GetComponent<Beam>().GetProperties();
                        foreach(bool i in beamList)
                        {
                            beamProperties.Append(i ? 1 : 2);
                        }
                        extraData.Add(Int32.Parse(beamProperties.ToString()));
                        extraData.Add(0);
                        break;
                    case "Goal":
                        id = 1;
                        extraData.Add(50);
                        extraData.Add(gameObj.transform.localScale.x);
                        break;
                    case "Wall":
                        id = 2;
                        extraData.Add(gameObj.transform.localScale.x);
                        extraData.Add(gameObj.transform.localScale.y);
                        break;
                    case "DragableForce":
                        id = 3;
                        extraData.Add(parseForceType(gameObj.GetComponent<Properties>().getType()));
                        extraData.Add(0); //If you change this, you have to zero this field in old level files
                        break;
                    case "DynamicForce":
                        id = 4;
                        extraData.Add(parseForceType(gameObj.GetComponent<DynamicProperties>().production));
                        extraData.Add(parseDynamicReaction(gameObj.GetComponent<DynamicProperties>().reaction));
                        break;
                    case "StaticForce":
                        id = 5;
                        extraData.Add(parseForceType(gameObj.GetComponent<Properties>().getType()));
                        extraData.Add(0); //If you change this, you have to zero this field in old level files
                        break;
                    case "Mirror":
                        id = 6;
                        extraData.Add(gameObj.transform.localScale.x);
                        extraData.Add(gameObj.transform.localScale.y);
                        break;
                    case "Wormhole":
                        id = 7;
                        extraData.Add(gameObj.GetComponent<Wormhole>().id);
                        extraData.Add(0);
                        break;
                    default:
                        id = -1;
                        Console.WriteLine("Whoops, something went wrong in SaveLevel.cs. The object type did not correspond with any preset options. " + gameObj.tag);
                        break;
                }

                if(id >= 0)
                {
                    sw.WriteLine(id);
                    sw.WriteLine(gameObj.transform.position.x);
                    sw.WriteLine(gameObj.transform.position.y);

                    if(GameProperties.previousLevel == "Editor" && gameObj.CompareTag("Beam"))
                    {
                        sw.WriteLine(gameObj.GetComponent<EditRotation>().getRotation());
                    }
                    else
                    {
                        sw.WriteLine(gameObj.transform.eulerAngles.z);
                    }

                    foreach(double i in extraData)
                    {
                        sw.WriteLine(i);
                    }
                }

            }
        
        sw.Close();
    }

    public void resetEditor()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, "Edit01.txt"));
    }
}
