using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceForce : MonoBehaviour {

	public bool canPlace = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		canPlace = false;

	}

	public void place(){

		canPlace = true;

	}
}
