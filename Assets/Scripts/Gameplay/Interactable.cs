using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    //drag and drop functionality
    protected Vector3 screenPoint;
    protected bool isDragged = false;
    protected Vector3 originalPos;
    protected int touchID = -1;

    public void onClick()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        originalPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isDragged = true;
    }

    public void onClick(int touchID)
    {
        this.touchID = touchID;
        Touch temp = getTouchByID(touchID);
        screenPoint = Camera.main.WorldToScreenPoint(temp.position);
        originalPos = Camera.main.ScreenToWorldPoint(new Vector3(temp.position.x, temp.position.y, screenPoint.z));
        isDragged = true;
    }

    protected Touch getTouchByID(int id)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).fingerId == id)
            {
                return Input.GetTouch(i);
            }
        }
        return Input.GetTouch(-1);
    }

    public void endClick()
    {
        isDragged = false;
        touchID = -1;
    }

    protected void Movement()
    {
        if (isDragged)
        {
            Touch touch = getTouchByID(touchID);
            Vector3 curPos = new Vector3(touch.position.x, touch.position.y, screenPoint.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
            GetComponent<Rigidbody2D>().MovePosition(worldPos);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().rotation = 0;
    }
}
