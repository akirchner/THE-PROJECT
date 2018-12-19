using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadIntoEditor : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Load()
    {
        string filepath;

        StreamWriter sw = File.CreateText(Path.Combine(Application.streamingAssetsPath, "Edit01.txt"));

        if (Application.platform == RuntimePlatform.Android)
        {
            filepath = Application.persistentDataPath + "/" + GameProperties.levelFilename;

            if (!File.Exists(filepath))
            {
                WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/" + GameProperties.levelFilename);
                while (!load.isDone) { }

                File.WriteAllBytes(filepath, load.bytes);
            }
        }
        else
        {
            filepath = Path.Combine(Application.streamingAssetsPath, GameProperties.levelFilename);
        }

        StreamReader sr = new StreamReader(filepath);

        string line = sr.ReadLine();

        while (line != null)
        {
            sw.WriteLine(line);
            line = sr.ReadLine();
        }

        sr.Close();
        sw.Close();

        GameProperties.previousLevel = GameProperties.currentLevel;
        GameProperties.currentLevel = "Editor";

        SceneManager.LoadScene("Editor");
    }
}
