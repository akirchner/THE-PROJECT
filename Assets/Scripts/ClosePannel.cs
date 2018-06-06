using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePannel : MonoBehaviour {

	public RectTransform panel, place;
	Vector2 offMin = new Vector2 (.82f,0f);
	Vector2 offMax = new Vector2 (1f,1f);
	Vector2 onMin = new Vector2 (.98f,0f);
	Vector2 onMax = new Vector2 (1.16f,1f);
	bool onScreen = true;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Close () {

		if (onScreen == true){

			panel.anchorMax = onMax;
			panel.anchorMin = onMin;

			onScreen = false;

		}

		else if(onScreen == false){

			panel.anchorMax = offMax;
			panel.anchorMin = offMin;

			onScreen = true;

		}
		Debug.Log (onScreen);
	
	}

}
