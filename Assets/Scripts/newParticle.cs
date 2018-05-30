using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newParticle : MonoBehaviour {

	public Text numberText;
	public Image forceSprite;
	public int numAvalible = 1;
	public bool isActive = false;
	public Transform force;
	Vector3 mousePos = new Vector3();
	Quaternion rotation = new Quaternion(0,0,0,0);

	//items for pixel to world unit conversion
	public Vector2 WorldUnitsInCamera;
	public Vector2 WorldToPixelAmount;
	public GameObject Camera;


	// Use this for initialization
	void Start () {

		numberText.text = numAvalible.ToString();
		forceSprite.sprite = force.GetComponent<SpriteRenderer>().sprite;

		//Finding Pixel To World Unit Conversion Based On Orthographic Size Of Camera
		WorldUnitsInCamera.y = Camera.GetComponent<Camera>().orthographicSize * 2;
		WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

		WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
		WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;
	}
	
	// Update is called once per frame
	void Update () {

		numberText.text = numAvalible.ToString();

		if (Input.GetMouseButton (0) && isActive == true) {
			mousePos = ConvertToWorldUnits(Input.mousePosition);
			Instantiate (force, mousePos, rotation);
			numAvalible--;
			isActive = false;
		}

	}
		
	public void Activate() {

		if (isActive == true) {

			isActive = false;

		}

		else if (numAvalible > 0) {
			
			isActive = true;

		}
	}

	//Taking Your Camera Location And Is Off Setting For Position And For Amount Of World Units In Camera
	public Vector2 ConvertToWorldUnits(Vector2 TouchLocation)
	{
		Vector2 returnVec2;

		returnVec2.x = ((TouchLocation.x / WorldToPixelAmount.x) - (WorldUnitsInCamera.x / 2)) +
			Camera.transform.position.x;
		returnVec2.y = ((TouchLocation.y / WorldToPixelAmount.y) - (WorldUnitsInCamera.y / 2)) +
			Camera.transform.position.y;

		return returnVec2;
	}
}
