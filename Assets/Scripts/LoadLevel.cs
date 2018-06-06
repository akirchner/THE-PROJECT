using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
    private List<List<double>> levelData;
    private List<double> currentObjectData;
    private List<bool> beamProperties;
    private string line, filepath;
    private Transform currentObject;
    public Transform dragableForce, dynamicForce, staticForce, goal, wall, beam, mirror;

	// Use this for initialization
	void Start () {
        try {
            levelData = new List<List<double>>();
            if (Application.platform == RuntimePlatform.Android) {
                filepath = Application.persistentDataPath + "/" + GameProperties.levelFilename;

                if (!File.Exists(filepath)) {
                    WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/" + GameProperties.levelFilename);
                    while (!load.isDone) { }

                    File.WriteAllBytes(filepath, load.bytes);
                }
            }
            else {
                filepath = Path.Combine(Application.streamingAssetsPath, GameProperties.levelFilename);
            }

            StreamReader sr = new StreamReader(filepath);

            GameObject.Find("GravitonButton").GetComponent<newParticle>().numAvailable = Int32.Parse(sr.ReadLine());
            GameObject.Find("ElectronButton").GetComponent<newParticle>().numAvailable = Int32.Parse(sr.ReadLine());
            GameObject.Find("FluxionButton").GetComponent<newParticle>().numAvailable = Int32.Parse(sr.ReadLine());

            line = sr.ReadLine();
            while(line != null) {
                currentObjectData = new List<double>();

                currentObjectData.Add(Double.Parse(line));
                currentObjectData.Add(Double.Parse(sr.ReadLine()));
                currentObjectData.Add(Double.Parse(sr.ReadLine()));
                currentObjectData.Add(Double.Parse(sr.ReadLine()));
                currentObjectData.Add(Double.Parse(sr.ReadLine()));
                currentObjectData.Add(Double.Parse(sr.ReadLine()));

                levelData.Add(currentObjectData);

                line = sr.ReadLine();
            }

            sr.Close();

            for (int i = 0; i < levelData.Count; i++) {
                switch ((int) levelData[i][0]) {
                case 0:
                    currentObject = Instantiate(beam, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                    beamProperties = new List<bool>();
                    foreach (char j in levelData[i][4].ToString()) {
                        beamProperties.Add(j.Equals('1'));
                    }
                    currentObject.GetComponent<Beam>().setProperites(beamProperties[0], beamProperties[1], beamProperties[2], beamProperties[3]);
                    break;
                case 1:
                    currentObject = Instantiate(goal, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                    currentObject.GetComponent<Finalization>().targetCount = (int)levelData[i][4];
                    currentObject.transform.localScale = new Vector3((float)levelData[i][5], 1, 1);
                    break;
                case 2:
                    currentObject = Instantiate(wall, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                    currentObject.transform.localScale = new Vector3((float)levelData[i][4], (float)levelData[i][5], 1);
                    break;
                case 3:
                    currentObject = Instantiate(dragableForce, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                    currentObject.GetComponent<Properties>().setType(parseForceType((int) levelData[i][4]));
                    currentObject.GetComponent<Properties>().size = (float) levelData[i][5];
                    break;
                case 4:
                    currentObject = Instantiate(dynamicForce, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                    currentObject.GetComponent<Properties>().setType(parseForceType((int)levelData[i][4]));
                    currentObject.GetComponent<Properties>().size = (float)levelData[i][5];
                    break;
                case 5:
                    currentObject = Instantiate(staticForce, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                    currentObject.GetComponent<Properties>().setType(parseForceType((int)levelData[i][4]));
                    currentObject.GetComponent<Properties>().size = (float)levelData[i][5];
                    break;
                case 6:
                    currentObject = Instantiate(mirror, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                    break;
                default:
                    Console.WriteLine("Whoops, something went wrong in LoadLevel.cs. The object ID did not correspond with any preset values.");
                    break;
                }                
            }
        }
        catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }
	}
	
    private ForceType parseForceType(int id) {
        switch(id) {
        case 1:
            return ForceType.Graviton;
        case 2:
            return ForceType.Fluxion;
        case 3:
            return ForceType.Electron;
        default:
            return ForceType.Empty;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
