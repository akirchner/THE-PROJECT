using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePannel : MonoBehaviour {

	public RectTransform panel;
	Vector3 shift = new Vector3(10,0,0);
	bool onScreen = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Close () {

		if (onScreen == true){
			
			panel.Translate(shift);
			onScreen = false;

		}

		else if(onScreen == false){
			
			panel.Translate(-shift);
			onScreen = true;

		}

	
	}

}
