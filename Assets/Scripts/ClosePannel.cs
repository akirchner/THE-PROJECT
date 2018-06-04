using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePannel : MonoBehaviour {

	public RectTransform panel;
    Vector2 retracted = new Vector2(35, 0);
    Vector2 extened = new Vector2(-40, 0);
	bool onScreen = true;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Close () {

		if (onScreen == true){

            panel.anchoredPosition = retracted;
			onScreen = false;

		}

		else if(onScreen == false){
			
            panel.anchoredPosition = extened;
			onScreen = true;

		}

	
	}

}
