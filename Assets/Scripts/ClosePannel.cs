using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePannel : MonoBehaviour {

	public RectTransform panel, place;
    Vector2 retracted = new Vector2(0, 0);
    Vector2 extened = new Vector2(0, 0);
	bool onScreen = true;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Close () {

		if (onScreen == true){

			panel.anchorMax.Set(1.16f, 1f);
			panel.anchorMin.Set(.2f, 0f);

			panel.offsetMax.Set(0,0);
			panel.offsetMin.Set(0, 0);

            //panel.anchoredPosition = retracted;
            //place.anchoredPosition = retracted;

	
			onScreen = false;

		}

		else if(onScreen == false){

			panel.anchorMax.Set(1f, 1f);
			panel.anchorMin.Set(.82f, 0f);


			//panel.anchoredPosition = extened;
            //place.anchoredPosition = extened;



			onScreen = true;

		}

	
	}

}
