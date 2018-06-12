using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

	public void LoadScene (string level){

        GameProperties.previousLevel = GameProperties.currentLevel;

        if (level.Substring(0, 4).ToUpper() == "PACK")
        {
            GameProperties.currentLevelPack = level.Substring(5, 1);
        }

        if (level == "CurrentPack")
        {
            if(GameProperties.currentLevelPack == "")
            {
                level = "Level Select";
            }
            else
            {
                level = "Pack " + GameProperties.currentLevelPack;
            }
        }

        if (level == "PreviousLevel")
        {
            level = GameProperties.previousLevel;
        }

		SceneManager.LoadScene (level);
        GameProperties.currentLevel = level;

	}

	public void SelectLevel (string filePath)
    {

		GameProperties.levelFilename = filePath;

        if (GameProperties.levelFilename == "")
        {
            Debug.Log("Hey! This level doesn't exist yet!");
            GameProperties.levelFilename = "defaultLevel.txt";
        }
        
	    LoadScene ("Level");

    }
}
