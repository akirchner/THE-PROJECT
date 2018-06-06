using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 screenPoint;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position -
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseDrag()
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); 
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        GetComponent<Rigidbody2D>().MovePosition(worldPos); 
    }

	private void Update()
	{	
	    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().rotation = 0;
	}
}