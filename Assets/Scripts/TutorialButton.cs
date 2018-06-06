using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour {

	public Transform close;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(close.GetComponent<ClosePannel>().onScreen == false){
			Delete ();
		}
			
	}

	public void Delete(){

		Destroy (this.gameObject);

	}

}
