using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDecoder : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DecodeLevel("pfABbkkQpAQdmcAeFxr", 2, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void DecodeLevel(string tempCode, int number1, int number2)
    {
        StreamWriter sw = File.CreateText(Path.Combine(Application.streamingAssetsPath, "User" + number1 + number2 + ".txt"));
        char[] code = tempCode.ToCharArray();
        int i = 0;

        //length checksum here

        if(LetToNum(code[i]) >= 12 && char.IsLower(code[i]))
        {
            int[] forces = ParseDrawer(code[i]);
            sw.WriteLine(forces[0]);
            sw.WriteLine(forces[1]);
            sw.WriteLine(forces[2]);
            i++;
        }
        else
        {
            sw.WriteLine(LetToNum(code[i]));
            sw.WriteLine(LetToNum(code[i + 1]));
            sw.WriteLine(LetToNum(code[i + 2]));
            i += 3;
        }

        while(i < code.Length - 1)
        {
            char tempID = code[i];
            float[] position = ParsePosition(code[i + 1], code[i + 2], code[i + 3]);
            double xPos = position[0];
            double yPos = position[1];
            i += 4;
            int id;
            double rotation, xData1, xData2;

            switch(tempID)
            {
                case 'A':
                case 'B':
                case 'C':
                    id = 5;
                    rotation = 0;
                    xData1 = ParseForceType(tempID);
                    xData2 = 0;
                    break;
                case 'D':
                case 'E':
                case 'F':
                    id = 3;
                    rotation = 0;
                    xData1 = ParseForceType(tempID);
                    xData2 = 0;
                    break;
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                    id = 4;
                    rotation = 0;
                    int[] type = ParseDynamicForce(tempID);
                    xData1 = type[0];
                    xData2 = type[1];
                    break;
                case 'O':
                    id = 6;
                    rotation = ParseRotation(code[i], code[i + 1]);
                    float[] scale = ParseScale(code[i + 2], code[i + 3]);
                    xData1 = scale[0];
                    xData2 = scale[1];
                    i += 4;
                    break;
                case 'P':
                    id = 2;
                    rotation = ParseRotation(code[i], code[i + 1]);
                    float[] scale2 = ParseScale(code[i + 2], code[i + 3]);
                    xData1 = scale2[0];
                    xData2 = scale2[1];
                    i += 4;
                    break;
                case 'Q':
                    id = 1;
                    rotation = ParseRotation(code[i], code[i + 1]);
                    xData1 = 50;
                    xData2 = ParseScale(code[i + 2], 'a')[0];
                    i += 3;
                    break;
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                    id = 7;
                    rotation = 0;
                    xData1 = LetToNum(tempID) - 18;
                    xData2 = 0;
                    break;
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                    id = 0;
                    rotation = ParseRotation(code[i], code[i + 1]);
                    i += 2;
                    xData1 = ParseBeam(tempID);
                    xData2 = 0;
                    break;
                default:
                    Debug.Log("Invalid ID character! Char: " + tempID);
                    id = -1;
                    rotation = -1;
                    xData1 = -1;
                    xData2 = -1;
                    break;
            }
            
            sw.WriteLine(id);
            sw.WriteLine(xPos);
            sw.WriteLine(yPos);
            sw.WriteLine(rotation);
            sw.WriteLine(xData1);
            sw.WriteLine(xData2);
            
        }

        sw.Close();
    }


    
    private int LetToNum(char c)
    {
        int value;

        if(char.IsLower(c))
        {
            value = c.GetHashCode() - 97;
        }
        else
        {
            value = c.GetHashCode() - 39;
        }

        return value;
    }



    private int ParseForceType(char c)
    {
        switch(c)
        {
            case 'A':
            case 'D':
                return 1;
            case 'B':
            case 'E':
                return 3;
            case 'C':
            case 'F':
                return 2;
            default:
                Debug.Log("Invalid force type! Char: " + c);
                return 0;
        }
    }



    private int[] ParseDynamicForce(char c)
    {
        int[] properties = new int[2];

        switch (c) {
            case 'G':
                properties[0] = 1;
                properties[1] = 2;
                break;
            case 'H':
                properties[0] = 1;
                properties[1] = 3;
                break;
            case 'I':
                properties[0] = 1;
                properties[1] = 4;
                break;
            case 'J':
                properties[0] = 3;
                properties[1] = 1;
                break;
            case 'K':
                properties[0] = 3;
                properties[1] = 2;
                break;
            case 'L':
                properties[0] = 2;
                properties[1] = 1;
                break;          
            case 'M':
                properties[0] = 2;
                properties[1] = 3;
                break;
            case 'N':
                properties[0] = 2;
                properties[1] = 4;
                break;
            default:
                Debug.Log("Invalid dynamic force! Char: " + c);
                break;
        }

        return properties;
    }



    private int ParseBeam(char c)
    {
        int properties;

        switch(c)
        {
            case 'a':
                properties = 2121;
                break;
            case 'b':
                properties = 2122;
                break;
            case 'c':
                properties = 2212;
                break;
            case 'd':
                properties = 2111;
                break;
            case 'e':
                properties = 2112;
                break;
            case 'f':
                properties = 1222;
                break;
            case 'g':
                properties = 1121;
                break;
            case 'h':
                properties = 1122;
                break;
            case 'i':
                properties = 1212;
                break;
            case 'j':
                properties = 1111;
                break;
            case 'k':
                properties = 1112;
                break;
            default:
                Debug.Log("Invalid beam id! Char: " + c);
                properties = 2222;
                break;
        }

        return properties;
    }



    private int[] ParseDrawer(char c)
    {
        int[] forces = new int[3];

        switch(c)
        {
            case 'l':
                forces[0] = 0;
                forces[1] = 0;
                forces[2] = 0;
                break;
            case 'm':
                forces[0] = 0;
                forces[1] = 0;
                forces[2] = 1;
                break;
            case 'n':
                forces[0] = 0;
                forces[1] = 1;
                forces[2] = 0;
                break;
            case 'o':
                forces[0] = 0;
                forces[1] = 1;
                forces[2] = 1;
                break;
            case 'p':
                forces[0] = 1;
                forces[1] = 0;
                forces[2] = 0;
                break;
            case 'q':
                forces[0] = 1;
                forces[1] = 0;
                forces[2] = 1;
                break;
            case 'r':
                forces[0] = 1;
                forces[1] = 1;
                forces[2] = 0;
                break;
            case 's':
                forces[0] = 1;
                forces[1] = 1;
                forces[2] = 1;
                break;
            default:
                Debug.Log("Invalid id in ParseDrawer. Char: " + c);
                break;
        }

        return forces;
    }



    private float[] ParsePosition(char xChar, char yChar, char dec)
    {
        float[] position = new float[2];
        float xDec = 0;
        float yDec = 0;

        position[0] = LetToNum(xChar);
        position[1] = LetToNum(char.ToLower(yChar));

        xDec = 0.2f * (LetToNum(char.ToLower(dec)) / 5);

        if (LetToNum(char.ToLower(dec)) % 5 == 0)
        {
            yDec = 0;
        }
        else if ((LetToNum(char.ToLower(dec)) - 1) % 5 == 0)
        {
            yDec = 0.2f;
        }
        else if ((LetToNum(char.ToLower(dec)) - 2) % 5 == 0)
        {
            yDec = 0.4f;
        }
        else if ((LetToNum(char.ToLower(dec)) - 3) % 5 == 0)
        {
            yDec = 0.6f;
        }
        else if ((LetToNum(char.ToLower(dec)) - 4) % 5 == 0)
        {
            yDec = 0.8f;
        }
        else
        {
            Debug.Log("Something went wrong in ParsePosition! Char: " + char.ToLower(dec));
        }

        position[0] += xDec;
        position[1] += yDec;

        if(char.IsLower(dec))
        {
            position[0] *= -1;
        }

        if(char.IsLower(yChar))
        {
            position[1] *= -1;
        }

        return position;
    }



    private float ParseRotation(char rot1, char rot2)
    {
        float rotation = 0;

        rotation += 26 * LetToNum(char.ToLower(rot1));
        rotation += LetToNum(char.ToLower(rot2));

        if(char.IsLower(rot1) && char.IsUpper(rot2))
        {
            rotation += 0.25f;
        }
        else if(char.IsUpper(rot1) && char.IsLower(rot2))
        {
            rotation += 0.5f;
        }
        else if(char.IsUpper(rot1) && char.IsUpper(rot2))
        {
            rotation += 0.75f;
        }

        return rotation;
    }



    private float[] ParseScale(char xChar, char yChar)
    {
        float[] scale = new float[2];

        scale[0] = LetToNum(char.ToLower(xChar));
        scale[1] = LetToNum(char.ToLower(yChar));

        if(char.IsUpper(xChar))
        {
            scale[0] += 0.5f;
        }

        if(char.IsUpper(yChar))
        {
            scale[1] += 0.5f;
        }

        return scale;
    }
}
