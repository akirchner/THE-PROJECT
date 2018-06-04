using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newParticle : MonoBehaviour {

	public Transform Place;
	public Text numberText;
	public Image forceSprite;
	public int numAvalible = 1;
	public Transform force;
	public Sprite swap;

	// Use this for initialization
	void Start () {

		numberText.text = numAvalible.ToString();
		forceSprite.sprite = force.GetComponent<SpriteRenderer>().sprite;

	}
	
	// Update is called once per frame
	void Update () {

		if (Place.GetComponent<PlaceForce> ().activeForce == force){
			
			if (Place.GetComponent<PlaceForce> ().decrment) {
				
				numAvalible--;
				Place.GetComponent<PlaceForce> ().decrment = false;
				Place.GetComponent<PlaceForce> ().activeForce = null;
			
			}
		}

		numberText.text = numAvalible.ToString();

		if(Place.GetComponent<PlaceForce> ().activeForce == force){

			forceSprite.sprite = swap;

		}
		else{

			forceSprite.sprite = force.GetComponent<SpriteRenderer>().sprite;

		}

	}

	public void Activate() {

		if (Place.GetComponent<PlaceForce> ().activeForce == force) {

			Place.GetComponent<PlaceForce> ().activeForce = null;

		} else if (numAvalible > 0) {
		
			Place.GetComponent<PlaceForce> ().activeForce = force;
		
		} 
		else {

			Place.GetComponent<PlaceForce> ().activeForce = null;

		}
	}

}
