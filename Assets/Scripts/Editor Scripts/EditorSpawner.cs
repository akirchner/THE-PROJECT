using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditorSpawner : MonoBehaviour, IPointerDownHandler {

	public bool hasSize;
	public Transform element, close;
	public ForceType type;
	int wormholeID = 0;

	Vector3 mousePos = new Vector3 ();
	Quaternion rotation = Quaternion.identity;

	//items for pixel to world unit conversion
	Vector2 WorldUnitsInCamera;
	Vector2 WorldToPixelAmount;
	public GameObject Camera;

	// Use this for initialization
	void Start () {
		
		//Finding Pixel To World Unit Conversion Based On Orthographic Size Of Camera
		WorldUnitsInCamera.y = Camera.GetComponent<Camera>().orthographicSize * 2;
		WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

		WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
		WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;

	}

	//detects the first half of a click, the pointer down.
	public void OnPointerDown(PointerEventData data){
		click (hasSize);

	}

	public void click(bool hasSize){
		if (hasSize) { //only scaleing objects like walls, goals and mirrors
			
			Instantiate(element, new Vector3(0,0,0), Quaternion.identity);
			close.GetComponent<ClosePannel>().Close();

		} 

		else { //only point objects like forces
			if(element.tag == "Wormhole"){
				
				close.GetComponent<ClosePannel>().Close();

				for (int i = 0; i < 2; i++){ //spawns two connected wormholes
					Transform temp;
					mousePos = ConvertToWorldUnits (Input.mousePosition); //finds mouse position
					temp = Instantiate (element, mousePos, rotation); //creates the force
					temp.GetComponent<Wormhole> ().id = wormholeID; //sets its type
					if(i == 1){
						temp.GetComponent<DragAndDrop> ().OnMouseDown (); //initiates dragging via DragAndDrop Script
					}
				}

				wormholeID++;

			}

			else if(element.tag == "Beam"){
				
			}

			else if(element.tag == "DynamicForce"){
                close.GetComponent<ClosePannel>().Close();
                Transform temp;
                mousePos = ConvertToWorldUnits(Input.mousePosition); //finds mouse position
                temp = Instantiate(element, mousePos, rotation); //creates the force
                temp.GetComponent<DynamicProperties>().production = GameObject.Find("Dynamic Force Panel").GetComponentInChildren<DynamicProperties>().production;
                temp.GetComponent<DynamicProperties>().reaction = GameObject.Find("Dynamic Force Panel").GetComponentInChildren<DynamicProperties>().reaction;

            }

            else {
				close.GetComponent<ClosePannel>().Close();
				Transform temp;
				mousePos = ConvertToWorldUnits (Input.mousePosition); //finds mouse position
				temp = Instantiate (element, mousePos, rotation); //creates the force
				temp.GetComponent<Properties> ().setType (type); //sets its type
				temp.GetComponent<DragAndDrop> ().OnMouseDown (); //initiates dragging via DragAndDrop Script

			}
		}

	}

	//converts pixel units to world units, I don't really know how
	public Vector2 ConvertToWorldUnits(Vector2 TouchLocation) {

		Vector2 returnVec2;

		returnVec2.x = ((TouchLocation.x / WorldToPixelAmount.x) - (WorldUnitsInCamera.x / 2)) +
			Camera.transform.position.x;
		returnVec2.y = ((TouchLocation.y / WorldToPixelAmount.y) - (WorldUnitsInCamera.y / 2)) +
			Camera.transform.position.y;

		return returnVec2;
	}
		
}
