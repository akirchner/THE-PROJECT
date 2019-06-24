using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadLevel : MonoBehaviour
{
    private List<List<double>> levelData;
    private List<double> currentObjectData;
    private List<bool> beamProperties;
    private string line, filepath;
    private Transform currentObject;
    public Transform dragableForce, dynamicForce, staticForce, goal, wall, beam, mirror, wormhole;

    // Use this for initialization
    void Start()
    {
        try
        {
            levelData = new List<List<double>>();
            if (Application.platform == RuntimePlatform.Android)
            {
                filepath = Application.persistentDataPath + "/" + GameProperties.levelFilename;

                if (!File.Exists(filepath))
                {
                    /*WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/" + GameProperties.levelFilename);
                    while (!load.isDone) { }

                    File.WriteAllBytes(filepath, load.bytes);*/

                    UnityWebRequest webReq = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets/" + GameProperties.levelFilename);
                    while(!webReq.isDone) { }
                    File.WriteAllBytes(filepath, webReq.downloadHandler.data);
              
                }
            }
            else
            {
                if (GameProperties.levelFilename.Substring(0, 4).Equals("User") || GameProperties.levelFilename.Substring(0, 4).Equals("Edit")) {
                    filepath = Path.Combine(Application.persistentDataPath, GameProperties.levelFilename);
                }
                else {
                    filepath = Path.Combine(Application.streamingAssetsPath, GameProperties.levelFilename);
                }
            }

            StreamReader sr = new StreamReader(filepath);

            GameObject.Find("GravitonButton").GetComponent<ForceSpawner>().numAvailable = Int32.Parse(sr.ReadLine());
            GameObject.Find("ElectronButton").GetComponent<ForceSpawner>().numAvailable = Int32.Parse(sr.ReadLine());
            GameObject.Find("FluxionButton").GetComponent<ForceSpawner>().numAvailable = Int32.Parse(sr.ReadLine());

            line = sr.ReadLine();
            while (line != null)
            {
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
            if (GameProperties.currentLevel == "Editor")
            {
                for (int i = 0; i < levelData.Count; i++)
                {
                    switch ((int)levelData[i][0])
                    {
                    case 0:
                        currentObject = Instantiate(beam, Vector3.zero, Quaternion.identity);
                        beamProperties = new List<bool>();
                        foreach (char j in levelData[i][4].ToString())
                        {
                            beamProperties.Add(j.Equals('1'));
                        }
                        currentObject.GetChild(0).GetComponent<Beam>().SetProperites(beamProperties[0], beamProperties[1], beamProperties[2], beamProperties[3]);
                        currentObject.GetChild(0).SetPositionAndRotation(new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.identity);
                        
                        float rotation = ((float)levelData[i][3]-90)*Mathf.Deg2Rad;
                    
                        currentObject.GetChild(1).SetPositionAndRotation(new Vector3(6f*Mathf.Cos(rotation) + (float)levelData[i][1], 6f*Mathf.Sin(rotation) + (float)levelData[i][2], 0), Quaternion.identity);
                        break;
                    case 1:
                        currentObject = Instantiate(goal, new Vector3(0, 0, 0), Quaternion.identity);

                        double offset = levelData[i][5] / (0.1709577 * 2);
                        currentObject.transform.GetChild(2).localPosition = new Vector3((float)(levelData[i][1] + (offset * Math.Cos(levelData[i][3] * Math.PI / 180))), (float)(levelData[i][2] + (offset * Math.Sin(levelData[i][3] * Math.PI / 180))), 0);
                        currentObject.transform.GetChild(1).localPosition = new Vector3((float)(levelData[i][1] - (offset * Math.Cos(levelData[i][3] * Math.PI / 180))), (float)(levelData[i][2] - (offset * Math.Sin(levelData[i][3] * Math.PI / 180))), 0);
                        break;
                    case 2:
                        currentObject = Instantiate(wall, new Vector3(0, 0, 0), Quaternion.identity);

                        currentObject.transform.GetChild(2).localPosition = new Vector3((float)(levelData[i][1] + (levelData[i][4] * Math.Cos(levelData[i][3] * Math.PI / 180))), (float)(levelData[i][2] + (levelData[i][4] * Math.Sin(levelData[i][3] * Math.PI / 180))), 0);
                        currentObject.transform.GetChild(1).localPosition = new Vector3((float)(levelData[i][1] - (levelData[i][4] * Math.Cos(levelData[i][3] * Math.PI / 180))), (float)(levelData[i][2] - (levelData[i][4] * Math.Sin(levelData[i][3] * Math.PI / 180))), 0);
                        break;
                    case 3:
                        currentObject = Instantiate(dragableForce, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.GetComponent<Properties>().setType(parseForceType((int)levelData[i][4]));
                        break;
                    case 4:
                        currentObject = Instantiate(dynamicForce, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.GetComponent<DynamicProperties>().production = parseForceType((int)levelData[i][4]);
                        currentObject.GetComponent<DynamicProperties>().reaction = parseDynamicReaction((int)levelData[i][5]);
                        break;
                    case 5:
                        currentObject = Instantiate(staticForce, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.GetComponent<Properties>().setType(parseForceType((int)levelData[i][4]));
                        break;
                    case 6:
                        currentObject = Instantiate(mirror, new Vector3(0, 0, 0), Quaternion.identity);

                        currentObject.transform.GetChild(1).localPosition = new Vector3((float)(levelData[i][1] + (levelData[i][5] * Math.Sin(levelData[i][3] * Math.PI / 180))), (float)(levelData[i][2] - (levelData[i][5] * Math.Cos(levelData[i][3] * Math.PI / 180))), 0);
                        currentObject.transform.GetChild(2).localPosition = new Vector3((float)(levelData[i][1] - (levelData[i][5] * Math.Sin(levelData[i][3] * Math.PI / 180))), (float)(levelData[i][2] + (levelData[i][5] * Math.Cos(levelData[i][3] * Math.PI / 180))), 0);
                        break;
                    case 7:
                        currentObject = Instantiate(wormhole, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.GetComponent<Wormhole>().id = (int)levelData[i][4];
                        break;
                    default:
                        Console.WriteLine("Whoops, something went wrong in LoadLevel.cs. The object ID did not correspond with any preset values.");
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < levelData.Count; i++)
                {
                    switch ((int)levelData[i][0])
                    {
                    case 0:
                        currentObject = Instantiate(beam, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        beamProperties = new List<bool>();
                        foreach (char j in levelData[i][4].ToString())
                        {
                            beamProperties.Add(j.Equals('1'));
                        }
                        currentObject.GetComponent<Beam>().SetProperites(beamProperties[0], beamProperties[1], beamProperties[2], beamProperties[3]);
                        break;
                    case 1:
                        currentObject = Instantiate(goal, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.transform.localScale = new Vector3((float)levelData[i][5], 1, 1);
                        break;
                    case 2:
                        currentObject = Instantiate(wall, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.transform.localScale = new Vector3((float)levelData[i][4], (float)levelData[i][5], 1);
                        break;
                    case 3:
                        currentObject = Instantiate(dragableForce, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.GetComponent<Properties>().setType(parseForceType((int)levelData[i][4]));
                        break;
                    case 4:
                        currentObject = Instantiate(dynamicForce, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.GetComponent<DynamicProperties>().production = parseForceType((int)levelData[i][4]);
                        currentObject.GetComponent<DynamicProperties>().reaction = parseDynamicReaction((int)levelData[i][5]);
                        break;
                    case 5:
                        currentObject = Instantiate(staticForce, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.GetComponent<Properties>().setType(parseForceType((int)levelData[i][4]));
                        break;
                    case 6:
                        currentObject = Instantiate(mirror, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.transform.localScale = new Vector3((float)levelData[i][4], (float)levelData[i][5], 1);
                        break;
                    case 7:
                        currentObject = Instantiate(wormhole, new Vector3((float)levelData[i][1], (float)levelData[i][2]), Quaternion.Euler(0, 0, (float)levelData[i][3]));
                        currentObject.GetComponent<Wormhole>().id = (int)levelData[i][4];
                        break;
                    default:
                        Console.WriteLine("Whoops, something went wrong in LoadLevel.cs. The object ID did not correspond with any preset values.");
                        break;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
    }

    private ForceType parseForceType(int id)
    {
        switch (id)
        {
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

    private ReactType parseDynamicReaction(int id)
    {
        switch (id)
        {
        case 1:
            return ReactType.Gravity;
        case 2:
            return ReactType.Flux;
        case 3:
            return ReactType.Positive;
        case 4:
            return ReactType.Negative;
        default:
            return ReactType.Gravity;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
