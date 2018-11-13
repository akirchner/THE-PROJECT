using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

	public void LoadScene (string level){

		if(level == "Editor/Share Level"){// this checks the old 'previous level' and thus does need to go here
			if (GameProperties.previousLevel == "Editor") {
				level = "Editor";
			}
			else{
				level = "Share Level";
			}
		}
		GameProperties.previousLevel = GameProperties.currentLevel;

        if (level.Substring(0, 4).ToUpper() == "PACK") {
            GameProperties.currentLevelPack = level.Substring(5, 1);
        }
        else if (level == "CurrentPack") {
            if (GameProperties.currentLevelPack == "") {
                level = "Level Select";
            }
            else {
                level = "Pack " + GameProperties.currentLevelPack;
            }
        }
        else if (level == "PreviousLevel") {
            level = GameProperties.previousLevel;
        }
        else if (level == "NextLevel") {
            if (Int32.Parse(GameProperties.levelFilename.Substring(4, 2)) == 15) {
                if (Int32.Parse(GameProperties.currentLevelPack) == GameProperties.saveData.GetLength(0) - 1) {
                    level = "Level Select";
                }
                else {
                    level = "Pack " + (Int32.Parse(GameProperties.currentLevelPack) + 1).ToString();
                }
            }
            else {
                GameProperties.levelFilename = GameProperties.levelFilename.Substring(0, 4) + (Int32.Parse(GameProperties.levelFilename.Substring(4, 2)) + 1).ToString().PadLeft(2, '0') + ".txt";
                level = "Level";
            }
        }
        else if (level =="Editor") {
            GameProperties.levelFilename = "editor.txt";
        }
        else if (level == "Editor Level") {
            GameObject.Find("Level Controller").GetComponent<SaveLevel>().save("Edit00.txt");
            GameProperties.levelFilename = "Edit00.txt";
        }

		SceneManager.LoadScene(level);
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
