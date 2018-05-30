using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceForce : MonoBehaviour {

	public bool canPlace = false;
	int i = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (i > 0) {
			i--;
		} 

		else {
			canPlace = false;
		}
	}

	public void place(){

		canPlace = true;
		i = 4;
	}
}
