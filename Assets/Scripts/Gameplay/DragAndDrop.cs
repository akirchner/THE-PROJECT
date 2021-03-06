﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class DragAndDrop : MonoBehaviour
{
    private Vector3 screenPoint;
	public bool isDragged = false;

	public void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		isDragged = true;
    }

	//void OnMouseUp(){
		
		//isDragged = false;
	
	//}

	private void Update()
	{	
		if (Input.GetMouseButtonUp(0)) {
			isDragged = false;
		}

		if (isDragged) {
			Vector3 curPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); 
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (curPos);
			GetComponent<Rigidbody2D> ().MovePosition (worldPos); 
		}

	    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().rotation = 0;
	}
}