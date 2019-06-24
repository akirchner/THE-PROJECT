using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelEncoder : MonoBehaviour
{
    private string filepath, line;
    private int roundUp, levelNumber1, levelNumber2;
    private InputField field;

    // Use this for initialization
    void Start()
    {
        field = GameObject.Find("InputField").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void EncodeLevel()
    {
        StringBuilder builder = new StringBuilder();
        GameProperties.levelFilename = "User" + levelNumber1 + levelNumber2 + ".txt";

        if(Application.platform == RuntimePlatform.Android)
        {
            filepath = Application.persistentDataPath + "/" + GameProperties.levelFilename;

            if(!File.Exists(filepath))
            {
                /*WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/" + GameProperties.levelFilename);
                while(!load.isDone) { }

                File.WriteAllBytes(filepath, load.bytes);*/
                UnityWebRequest webReq = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets/" + GameProperties.levelFilename);
                while(!webReq.isDone) { }
                File.WriteAllBytes(filepath, webReq.downloadHandler.data);
            }
        }
        else
        {
                filepath = Path.Combine(Application.persistentDataPath, GameProperties.levelFilename);
        }

        StreamReader sr = new StreamReader(filepath);

        line = sr.ReadLine();
        List<string> levelData = new List<string>();

        while (line != null)
        {
            levelData.Add(line);
            line = sr.ReadLine();
        }

        sr.Close();

        if(Int32.Parse(levelData[0]) < 2 && Int32.Parse(levelData[1]) < 2 && Int32.Parse(levelData[2]) < 2)
        {
            builder.Append(ParseDrawer(Int32.Parse(levelData[0]), Int32.Parse(levelData[1]), Int32.Parse(levelData[2])));
        }
        else
        {
            builder.Append(NumToLet((Int32.Parse(levelData[0]) % 26) + 1, Int32.Parse(levelData[0]) > 25));
            builder.Append(NumToLet((Int32.Parse(levelData[1]) % 26) + 1, Int32.Parse(levelData[1]) > 25));
            builder.Append(NumToLet((Int32.Parse(levelData[2]) % 26) + 1, Int32.Parse(levelData[2]) > 25));
        }

        

        for (int i = 3; i < levelData.Count; i++)
        {
            int tempID = Int32.Parse(levelData[i]);
            string wormholeID = "-";
            float tempX = float.Parse(levelData[i + 1]);
            float tempY = float.Parse(levelData[i + 2]);
            float tempRot = float.Parse(levelData[i + 3]);
            float xData1 = float.Parse(levelData[i + 4]);
            float xData2 = float.Parse(levelData[i + 5]);
            i += 5;

            string id = "#";
            string[] position = ParsePosition(tempX, tempY);
            string[] rotation = ParseRotation(tempRot);
            string[] scale = new string[2];

            switch (tempID)
            {
                case 0: //Beam
                    bool[] properties = new bool[4];

                    for(int j = 0; j < properties.Length; j++)
                    {
                        properties[j] = xData1.ToString().ToCharArray()[j] == '1';
                    }

                    id = ParseBeam(properties);
                    break;
                case 1: //Goal
                    id = "Q";
                    scale = ParseScale(xData2, 1);
                    break;
                case 2: //Wall
                    id = "P";
                    scale = ParseScale(xData1, xData2);
                    break;
                case 3: //Dragable
                    switch (xData1.ToString())
                    {
                        case "1":
                            id = "D";
                            break;
                        case "2":
                            id = "F";
                            break;
                        case "3":
                            id = "E";
                            break;
                        default:
                            Debug.Log("Invalid dragable force type! Type: " + xData1);
                            break;

                    }
                    break;
                case 4: //Dynamic
                    string production = "#";
                    string reaction = "#";

                    switch(xData1.ToString())
                    {
                        case "1":
                            production = "G";
                            break;
                        case "2":
                            production = "F";
                            break;
                        case "3":
                            production = "E";
                            break;
                        default:
                            Debug.Log("Invalid dynamic production type! Type: " + xData1);
                            break;
                    }

                    switch(xData2.ToString())
                    {
                        case "1":
                            reaction = "G";
                            break;
                        case "2":
                            reaction = "F";
                            break;
                        case "3":
                            reaction = "P";
                            break;
                        case "4":
                            reaction = "N";
                            break;
                        default:
                            Debug.Log("Invalid dynamic reaction type! Type: " + xData2);
                            break;
                    }

                    id = ParseDynamicForce(production, reaction);

                    break;
                case 5: //Static
                    switch(xData1.ToString())
                    {
                        case "1":
                            id = "A";
                            break;
                        case "2":
                            id = "C";
                            break;
                        case "3":
                            id = "B";
                            break;
                        default:
                            Debug.Log("Invalid static force type! Type: " + xData1);
                            break;

                    }
                    break;
                case 6: //Mirror
                    id = "O";
                    scale = ParseScale(xData1, xData2);
                    break;
                case 7: //Wormhole
                    id = "R";
                    wormholeID = NumToLet(xData1 + 1, true);
                    break;
                default:
                    Debug.Log("Invalid ID in LevelEncoder! ID: " + tempID);
                    break;
            }

            builder.Append(id);
            builder.Append(position[0]);
            builder.Append(position[1]);
            builder.Append(position[2]);

            switch(tempID) {
                case 0: //Beam
                    builder.Append(rotation[0]);
                    builder.Append(rotation[1]);
                    break;
                case 1: //Goal
                    builder.Append(rotation[0]);
                    builder.Append(rotation[1]);
                    builder.Append(scale[0]);
                    break;
                case 2: //Wall
                case 6: //Mirror
                    builder.Append(rotation[0]);
                    builder.Append(rotation[1]);
                    builder.Append(scale[0]);
                    builder.Append(scale[1]);
                    break;
                case 7:
                    builder.Append(wormholeID);
                    break;
                default:
                    break;
            }

        }

        builder.Append(NumToLet((builder.Length % 26) + 1, true));
        field.text = builder.ToString();
        GameProperties.levelcode = builder.ToString();
    }



    private string ParseBeam(bool[] properties)
    {
        int value = 0;

        if(properties[0])
        {
            value += 6;
        }
        if(properties[2])
        {
            value += 3;
        }
        if(properties[1])
        {
            if(properties[3])
            {
                value += 1;
            }
            else
            {
                value += 2;
            }
        }

        string id = NumToLet(value + 1, false);
        return id;
    }



    private string ParseDynamicForce(string production, string reaction)
    {
        string id = "#";

        switch (production + reaction) {
            case "GF":
                id = "G";
                break;
            case "GP":
                id = "H";
                break;
            case "GN":
                id = "I";
                break;
            case "EG":
                id = "J";
                break;
            case "EF":
                id = "K";
                break;
            case "FG":
                id = "L";
                break;
            case "FP":
                id = "M";
                break;
            case "FN":
                id = "N";
                break;
            default:
                Debug.Log("Invalid production/reaction! PR: " + production + reaction);
                break;
        }

        return id;
    }



    private string ParseDrawer(int g, int e, int f)
    {
        string id = "#";

        
        switch(g)
        {
            case 0:
                switch (e)
                {
                    case 0:
                        switch (f)
                        {
                            case 0:
                                id = NumToLet(12, false);
                                break;
                            case 1:
                                id = NumToLet(13, false);
                                break;
                        }
                        break;
                    case 1:
                        switch (f)
                        {
                            case 0:
                                id = NumToLet(14, false);
                                break;
                            case 1:
                                id = NumToLet(15, false);
                                break;
                        }
                        break;
                }
                break;
            case 1:
                switch (e)
                {
                    case 0:
                        switch (f)
                        {
                            case 0:
                                id = NumToLet(16, false);
                                break;
                            case 1:
                                id = NumToLet(17, false);
                                break;
                        }
                        break;
                    case 1:
                        switch (f)
                        {
                            case 0:
                                id = NumToLet(18, false);
                                break;
                            case 1:
                                id = NumToLet(19, false);
                                break;
                        }
                        break;
                }
                break;
        }

        return id;
    }



    private string[] ParsePosition(float x, float y)
    {
        string[] position = new string[3];
        float xDec = ToNearestFifth(Math.Abs(x));
        float xDecOrg = Math.Abs(x) % 1;
        if(x > 0)
        {
            x += roundUp;
        }
        else
        {
            x -= roundUp;
        }

        float yDec = ToNearestFifth(Math.Abs(y));
        float yDecOrg = Math.Abs(y) % 1;

        if (y > 0)
        {
            y += roundUp;
        }
        else
        {
            y -= roundUp;
        }

        if(xDec == 0)
        {
            x = (float) Math.Round(x);
        }

        if (yDec == 0)
        {
            y = (float) Math.Round(y);
        }

        position[0] = NumToLet((Mathf.Round((Math.Abs(x) - xDecOrg)) % 26) + 1, Math.Abs(x) > 25.5);
        position[1] = NumToLet((Mathf.Round(Math.Abs(y) - yDecOrg)) + 1, y > 0);

        int decimalSearcher = 0;

        while(xDec > 0.2)
        {
            decimalSearcher += 5;
            xDec -= 0.2f;
        }

        while(yDec > 0.2)
        {
            decimalSearcher++;
            yDec -= 0.2f;
        }

        position[2] = NumToLet(decimalSearcher + 1, x >= 0);

        return position;
    }

    

    private string[] ParseRotation(float degreeRot)
    {
        string[] rotation = new string[2];
        degreeRot %= 360;

        if(degreeRot < 0)
        {
            degreeRot = 360 + degreeRot;
        }

        float degDec = ToNearestQuarter(degreeRot % 1);
        degreeRot += roundUp;

        rotation[0] = NumToLet((degreeRot / 26) + 1, (degDec == 0.5 || degDec == 0.75));
        rotation[1] = NumToLet((degreeRot % 26) + 1, (degDec == 0.25 || degDec == 0.75));

        return rotation;
    }

    

    private string[] ParseScale(float xScl, float yScl)
    {
        string[] scale = new string[2];
        xScl = Math.Abs(xScl);
        yScl = Math.Abs(yScl);

        float xDec = ToNearestHalf(xScl % 1);
        xScl += roundUp;
        float yDec = ToNearestHalf(yScl % 1);
        yScl += roundUp;

        scale[0] = NumToLet((xScl % 26) + 1, xDec == 0.5);
        scale[1] = NumToLet((yScl % 26) + 1, yDec == 0.5);

        return scale;
    }



    private string NumToLet(float number, bool isCaps)
    {
        char c = (char)((isCaps ? 65 : 97) + (int) (number - 1));
        return c.ToString();
    }



    private float ToNearestFifth(float number)
    {
        number %= 1;

        if (number >= 0.9)
        {
            roundUp = 1;
        }
        else
        {
            roundUp = 0;
        }

        if (number >= 0.9 || number < 0.1)
        {
            number = 0f;
        }
        else if(number >= 0.1 && number < 0.3)
        {
            number = 0.2f;
        }
        else if(number >= 0.3 && number < 0.5)
        {
            number = 0.4f;
        }
        else if(number >= 0.5 && number < 0.7)
        {
            number = 0.6f;
        }
        else
        {
            number = 0.8f;
        }

        return number;
    }

    private float ToNearestQuarter(float number)
    {
        number %= 1;

        if(number >= 0.875)
        {
            roundUp = 1;
        }
        else
        {
            roundUp = 0;
        }

        if(number >= 0.875 || number < 0.125)
        {
            number = 0f;
        }
        else if(number >= 0.125 && number < 0.375)
        {
            number = 0.25f;
        }
        else if(number >= 0.375 && number < 0.625)
        {
            number = 0.5f;
        }
        else
        {
            number = 0.75f;
        }

        return number;
    }

    private float ToNearestHalf(float number)
    {
        number %= 1;

        if (number >= 0.75)
        {
            roundUp = 1;
        }
        else
        {
            roundUp = 0;
        }

        if (number >= 0.75 || number < 0.25)
        {
            number = 0f;
        }
        else
        {
            number = 0.5f;
        }

        return number;
    }

    public void SetLevel(int number)
    {
        levelNumber1 = number / 10;
        levelNumber2 = number % 10;
        EncodeLevel();
    }
}
