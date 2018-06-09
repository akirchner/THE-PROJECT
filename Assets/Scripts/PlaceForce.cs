using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceForce : MonoBehaviour {

	public Transform force;
	public ForceType activeForce = ForceType.Empty;
	Vector3 mousePos = new Vector3();
    Quaternion rotation = Quaternion.identity;
	public bool decrment = false;

	//items for pixel to world unit conversion
	public Vector2 WorldUnitsInCamera;
	public Vector2 WorldToPixelAmount;
	public GameObject Camera;

	// Use this for initialization
	void Start () {
		
		//Finding Pixel To World Unit Conversion Based On Orthographic Size Of Camera
		WorldUnitsInCamera.y = Camera.GetComponent<Camera>().orthographicSize * 2;
		WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

		WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
		WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void place(){

		if (activeForce != ForceType.Empty) {

			Transform temp;
			decrment = true;
			mousePos = ConvertToWorldUnits (Input.mousePosition);
            temp = Instantiate (force, mousePos, rotation);
			temp.GetComponent<Properties>().setType (activeForce);
            //update here
            GameObject.FindGameObjectsWithTag("Beam")[0].GetComponent<Beam>().UpdateForces();


		}

	}

	public Vector2 ConvertToWorldUnits(Vector2 TouchLocation) {
		
		Vector2 returnVec2;

		returnVec2.x = ((TouchLocation.x / WorldToPixelAmount.x) - (WorldUnitsInCamera.x / 2)) +
			Camera.transform.position.x;
		returnVec2.y = ((TouchLocation.y / WorldToPixelAmount.y) - (WorldUnitsInCamera.y / 2)) +
			Camera.transform.position.y;

		return returnVec2;
	}

}
