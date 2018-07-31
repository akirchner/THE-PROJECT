using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSave : MonoBehaviour {
    public int packID, levelID;
    public Transform check;

	// Use this for initialization
	void Start () {
		if (GameProperties.saveData[packID, levelID]) {
            this.transform.Find("CheckMark").GetComponent<Image>().enabled = true;
        }
        else {
            this.transform.Find("CheckMark").GetComponent<Image>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
