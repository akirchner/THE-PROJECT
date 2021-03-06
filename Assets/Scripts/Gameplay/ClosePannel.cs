﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePannel : MonoBehaviour {

	public RectTransform panel,subImage;
	public Vector2 offMin = new Vector2 (.82f,0f);
	public Vector2 offMax = new Vector2 (1f,1f);
	public Vector2 onMin = new Vector2 (.98f,0f);
	public Vector2 onMax = new Vector2 (1.16f,1f);
	public bool onScreen;

	// Use this for initialization
	void Start () {
		Close ();
    }
		
	public void Close () {

		if (onScreen == false){
			panel.anchorMax = onMax;
			panel.anchorMin = onMin;
			if (subImage != null) {
				subImage.rotation = Quaternion.Euler (0f, 0f, 180f);
			}

			onScreen = true;

		}

		else if(onScreen == true){

			panel.anchorMax = offMax;
			panel.anchorMin = offMin;
			if (subImage != null) {
				subImage.rotation = Quaternion.Euler (0f, 0f, 0f);
			}

			onScreen = false;

		}
	
	}

}
