using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetProgress : MonoBehaviour {
    StreamWriter sw;

    // Use this for initialization
    public void Reset () {
        sw = File.CreateText(Path.Combine(Application.persistentDataPath, "gameData.txt"));

        for (int i = 0; i < 10; i++) { //Don't use length becuase it gives the total amount of elements in the array.
            for (int j = 0; j < 15; j++) {
                GameProperties.saveData[i, j] = false;
                sw.WriteLine(0);
            }
        }

        sw.Close();
		SceneManager.LoadScene ("Main Menu");
    }

}
