using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class newParticle : MonoBehaviour,  IPointerDownHandler {

	//all the sprites
	public Sprite gravSprite, elecSprite, fluxSprite, hGravSprite, hElecSprite, hFluxSprite, transparentSprite;

	public Transform closeButton;
	public Text numberText;
	public Image forceSprite;
	public int numAvailable = 1;
	public ForceType type;


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
        
		activeForce = type;
		numberText.text = numAvailable.ToString();
		setSprite ();

		//Finding Pixel To World Unit Conversion Based On Orthographic Size Of Camera
		WorldUnitsInCamera.y = Camera.GetComponent<Camera>().orthographicSize * 2;
		WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

		WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
		WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;


	}
	
	// Update is called once per frame
	void Update () {

		if (activeForce == type){
			
			if (decrment) {
				
				numAvailable--;
				decrment = false;
				activeForce = ForceType.Empty;
			
			}
		}

		setSprite ();
        if (numAvailable > 0) {
            numberText.text = numAvailable.ToString();
        }
        else {
            numberText.text = "";
        }

    }

	//detects the first half of a click, the pointer down.
	public void OnPointerDown(PointerEventData data){
		
		closeButton.GetComponent<ClosePannel> ().Close();
		place ();

	}

	public void place(){

		if (activeForce != ForceType.Empty) {

			Transform temp;
			decrment = true;
			mousePos = ConvertToWorldUnits (Input.mousePosition);
			temp = Instantiate (force, mousePos, rotation);
			temp.GetComponent<Properties>().setType (activeForce);
			temp.GetComponent<DragAndDrop> ().click ();

		}

	}

	void setSprite (){

        if (numAvailable > 0) {
            /*if (Place.GetComponent<PlaceForce>().activeForce == type) {

                switch (type) {
                case ForceType.Graviton:
                    this.GetComponent<Image>().sprite = hGravSprite;
                    break;
                case ForceType.Fluxion:
                    this.GetComponent<Image>().sprite = hFluxSprite;
                    break;
                case ForceType.Electron:
                    this.GetComponent<Image>().sprite = hElecSprite;
                    break;
                }

            }
            else {*/

                switch (type) {
                case ForceType.Graviton:
                    this.GetComponent<Image>().sprite = gravSprite;
                    break;
                case ForceType.Fluxion:
                    this.GetComponent<Image>().sprite = fluxSprite;
                    break;
                case ForceType.Electron:
                    this.GetComponent<Image>().sprite = elecSprite;
                    break;
                }
            //}
            this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else {
            this.GetComponent<Image>().color = new Color (0f, 0f, 0f, 0f);
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
