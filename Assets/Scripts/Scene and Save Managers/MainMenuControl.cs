using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

	public void LoadScene (string level){
		//loads the given scene, except for several exeptions:

		//handles the back button in scene: Editor Save, which leads to different scenes depending on where you came from
		if(level == "Editor/Share Level"){// this checks the old 'previous level' and thus does need to go here
			if (GameProperties.previousLevel == "Editor") {
				level = "Editor";
			}
			else{
				level = "Share Level";
			}
		}

		GameProperties.previousLevel = GameProperties.currentLevel;

		//saves the current pack number for future reference
        if (level.Substring(0, 4).ToUpper() == "PACK") {
            GameProperties.currentLevelPack = level.Substring(5, 1);
        }
		//loads the current pack based on above
        else if (level == "CurrentPack") {
            if (GameProperties.currentLevelPack == "") {
                level = "Level Select";
            }
            else {
                level = "Pack " + GameProperties.currentLevelPack;
            }
        }
		//handles the replay button
        else if (level == "PreviousLevel") {
            level = GameProperties.previousLevel;
        }
		//handles the next level button
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
		//sets the level file to the editor temp file when the editor is loaded
        else if (level =="Editor") {
            GameProperties.levelFilename = "Edit00.txt";
        }
		//saves the editor temp file and loads it into editor level
        else if (level == "Editor Level") {
            GameObject.Find("Level Controller").GetComponent<SaveLevel>().save("Edit00.txt");
            GameProperties.levelFilename = "Edit00.txt";
        }

		//main case of LoadScene
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
