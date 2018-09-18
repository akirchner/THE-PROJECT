using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProperties {
	public static string levelFilename;
    public static string previousLevel;
    public static string currentLevel;
    public static string currentLevelPack = "";
    public static int currentTutorial;
    public static bool[,] saveData = new bool[7, 15];
    public static bool bigfalconbeam = false;
}
