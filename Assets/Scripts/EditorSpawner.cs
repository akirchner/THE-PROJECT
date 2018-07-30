using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditorSpawner : MonoBehaviour, IPointerDownHandler {

	public bool hasSize;
	public Transform element, close;
	public ForceType type;

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
		Debug.Log ("clicked");
		click (hasSize);

	}

	public void click(bool hasSize){
		if (hasSize) { //only scaleing objects like walls
			
			Instantiate(element, new Vector3(0,0,0), Quaternion.identity);
			close.GetComponent<ClosePannel>().Close();

		} 

		else { //only point objects like forces

			close.GetComponent<ClosePannel>().Close();
			Transform temp;
			mousePos = ConvertToWorldUnits (Input.mousePosition); //finds mouse position
			temp = Instantiate (element, mousePos, rotation); //creates the force
			temp.GetComponent<Properties> ().setType (type); //sets its type
			temp.GetComponent<DragAndDrop> ().OnMouseDown (); //initiates dragging via DragAndDrop Script

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
