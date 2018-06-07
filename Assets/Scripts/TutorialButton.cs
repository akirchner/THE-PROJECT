using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour {

	public Transform close;
    GameObject buttonToHide;

	// Use this for initialization
	void Start () {
            Status(0);
	}
	
	// Update is called once per frame
	void Update () {

        if(close.GetComponent<ClosePannel>().onScreen == false){
            Status(1);
		}		
	}

    public void Status(int stage)
    {

        switch (stage)
        {
            case 0:
                if (name.Equals("Button 2"))
                {
                    GetComponent<Transform>().localScale = (new Vector3(0, 0, 0));
                } else {
                    GetComponent<Transform>().localScale = (new Vector3(1, 1, 0));
                }
                break;
            case 1:
                if (name.Equals("Button 2")) {
                    GetComponent<Transform>().localScale = (new Vector3(1, 1, 0));
                } else {
                    GetComponent<Transform>().localScale = (new Vector3(0, 0, 0));
                }
                break;
        } 
    }
}
