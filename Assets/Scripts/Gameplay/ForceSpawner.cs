using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ForceSpawner : MonoBehaviour, IPointerDownHandler {

	public bool isEditor;
	//all the sprites
	public Sprite gravSprite, elecSprite, fluxSprite;

	public Transform closeButton;
	public Text numberText;
	public int numAvailable = 1;
	public ForceType type = ForceType.Empty;


	public Transform force;
	Vector3 mousePos = new Vector3();
	Quaternion rotation = Quaternion.identity;

	//items for pixel to world unit conversion
	Vector2 WorldUnitsInCamera;
	Vector2 WorldToPixelAmount;
	public GameObject Camera;

	// Use this for initialization
	void Start () {
        
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

		setSprite ();

		if (numAvailable > 0) {
			//displays the number of a certain force remaining in the "Bank"
			numberText.text = numAvailable.ToString ();
		} 
		else if (numAvailable == 0 && isEditor) {
			numberText.text = "0";
		}
        else {
			numAvailable = 0;
            numberText.text = "";
        }

    }

	//detects the first half of a click, the pointer down.
	public void OnPointerDown(PointerEventData data){

		if (isEditor) {
		
		} 
		else {
			place ();
		}
	}

    //closes the force drawer and places a force where the button used to be
    public void place(){

		if(numAvailable > 0) {
            closeButton.GetComponent<ClosePannel>().Close();

            //creates the new force at the current mouse position, sets its type to match the button, and makes it dragable 
            Transform temp;
            numAvailable--; //removes a force from the "bank"
			mousePos = ConvertToWorldUnits (Input.mousePosition); //finds mouse position
			temp = Instantiate (force, mousePos, rotation); //creates the force
			temp.GetComponent<Properties> ().setType (type); //sets its type
			temp.GetComponent<DragAndDrop> ().OnMouseDown (); //initiates dragging via DragAndDrop Script

		}

	}

	void setSprite (){
		//sets the sprite to the proper image based on the type enum.
        if (numAvailable > 0) {
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
                default:
                    Debug.Log("Invalid ForceType while setting sprite! ForceSpawner.cs");
                    break;
                }
			//shows the image by setting its opacity to full
            this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        }
        else {
			//hides the empty button by setting its opacity to 0
			if (isEditor) {
			} 
			else {
				this.GetComponent<Image> ().color = new Color (0f, 0f, 0f, 0f);
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
