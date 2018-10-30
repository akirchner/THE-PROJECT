using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class OnTouch : MonoBehaviour {

	public GameObject touchParticles;

	private Vector3 screenPoint;

	public void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			touchParticles.GetComponent<ParticleSystem> ().Play ();
		
				// Convert to world units
				Vector3 curPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); 
				Vector3 worldPos = Camera.main.ScreenToWorldPoint (curPos);
				GetComponent<Rigidbody2D> ().MovePosition (worldPos); 

				touchParticles.transform.position = worldPos;



		}
	}
}
