using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePannel : MonoBehaviour {

	public RectTransform panel, place;
    public Vector2 retracted = new Vector2(35, 0);
    Vector2 extened = new Vector2(-45, 0);
	bool onScreen = true;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Close () {

		if (onScreen == true){
            place.offsetMax.Set(0, place.offsetMax.y);
            place.offsetMin.Set(0, place.offsetMin.y);

            panel.anchoredPosition = retracted;
            place.anchoredPosition = retracted;
			onScreen = false;

		}

		else if(onScreen == false){
			
            panel.anchoredPosition = extened;
            place.anchoredPosition = extened;
			onScreen = true;

		}

	
	}

}
