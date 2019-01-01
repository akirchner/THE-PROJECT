using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMusicLocation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetLocation(int location)
    {
        switch (location)
        {
            case 1:
                GameObject.Find("Music").GetComponent<Music>().currentLocation = Music.Location.GRAV;
                break;
            case 2:
                GameObject.Find("Music").GetComponent<Music>().currentLocation = Music.Location.ELEC;
                break;
            case 3:
                GameObject.Find("Music").GetComponent<Music>().currentLocation = Music.Location.FLUX;
                break;
            case 4:
                GameObject.Find("Music").GetComponent<Music>().currentLocation = Music.Location.MIXED;
                break;
            case 5:
                GameObject.Find("Music").GetComponent<Music>().currentLocation = Music.Location.WORMHOLE;
                break;
            case 6:
                GameObject.Find("Music").GetComponent<Music>().currentLocation = Music.Location.DYNAMIC;
                break;
            case 7:
                GameObject.Find("Music").GetComponent<Music>().currentLocation = Music.Location.EDITOR;
                break;
            default:
                Debug.Log("Invalid setlocation for music! " + location);
                break;
        }
    }
}
