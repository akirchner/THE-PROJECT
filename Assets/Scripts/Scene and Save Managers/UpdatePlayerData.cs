using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerData : MonoBehaviour {

	// Use this for initialization
	void Start () {
        try
        {
            string filepath = Path.Combine(Application.persistentDataPath, "playerData.txt");

            if (File.Exists(filepath))
            {
                StreamReader sr = new StreamReader(filepath);

                for(int i = 0; i < 15; i++)
                {
                    GameProperties.playerLevelData[i] = System.Int32.Parse(sr.ReadLine()) == 1;
                }

                sr.Close();
            }
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("No player levels save file found!");
        }
    }

    // Update is called once per frame
    public void Update()
    {
        for(int i = 0; i < 15; i++)
        {
            if(GameProperties.playerLevelData[i])
            {
                GameObject.Find("Level Button " + (i + 1)).GetComponent<Image>().color = Color.white;
            }
            else
            {
                GameObject.Find("Level Button " + (i + 1)).GetComponent<Image>().color = Color.grey;
            }
        }
    }

    public void newLevel(int levelNum)
    {
        GameProperties.playerLevelData[levelNum - 1] = true;
        GameProperties.saveData[9, levelNum - 1] = false;
    }

    public void save()
    {
        StreamWriter sw = File.CreateText(Path.Combine(Application.persistentDataPath, "playerData.txt"));

        for (int i = 0; i < 15; i++)
        {
            if(GameProperties.playerLevelData[i])
            {
                sw.WriteLine("1");
            }
            else
            {
                sw.WriteLine("0");
            }
        }

        sw.Close();
    }
}
