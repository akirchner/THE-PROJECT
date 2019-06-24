using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * script for managing the tutorial text boxes, supports up to two textboxes per level.
 * each button can be left unassigned if it doesnt need to change in the scene.
 * 
 */


public class TutorialButton : MonoBehaviour {

	public int currentStatus = 1;
    
    // graphical elemets to be affected, leave blank if not needed
	public Transform close;
    public GameObject button1,button2,nextButton,target;

	// Use this for initialization
	void Start () {
		Status(currentStatus);
	}

	public void Next(){
        // functions called by clicking the 'next' button

		currentStatus++;
		Status(currentStatus);
	
	}

    public void Status(int stage) {

        switch (stage) {
			case 1:
                if(button1 != null)
                {
                    //turn button1 on by settting its scale to 1
                    button1.GetComponent<Transform>().localScale = (new Vector3(1, 1, 0));
                }

                if (target != null)
                {
                    //move target off of screen
                    target.GetComponent<Transform>().position = (new Vector3(1000, 0, 0));
                }
                break;

		case 2:
				
						//turn button1 off by setting its scale to 0
			button1.GetComponent<Transform> ().localScale = (new Vector3 (0, 0, 0));
			if (button2 != null) {
				//turn button2 on by setting its scale to 1
				button2.GetComponent<Transform> ().localScale = (new Vector3 (1, 1, 0));
			}
                //turn nextButton off by setting its scale to 0
                nextButton.GetComponent<Transform> ().localScale = (new Vector3 (0, 0, 0));

			if (target != null) {
				//move target onto screen
				target.GetComponent<Transform> ().position = (new Vector3 (-10, 12, 0));
			}
                if (close != null) {
                    // open the force drawer
                    close.GetComponent<ClosePannel>().Close();
                }
                break;
			default:
				break;
        } 
    }
}
