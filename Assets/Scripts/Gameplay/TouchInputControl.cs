using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    RaycastHit2D getTouchTarget(Vector3 position)
    {
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(position);
        Vector2 touchPos2D = new Vector2(touchPos.x, touchPos.y);
        return Physics2D.Raycast(touchPos2D, Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
           {
                RaycastHit2D hit = getTouchTarget(Input.GetTouch(i).position);
                if (hit.collider != null)
                {
                    hit.transform.GetComponent<Interactable>().onClick(i);
                    hit.transform.GetComponent<PlaySound>().PlayClip();
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                RaycastHit2D hit = getTouchTarget(Input.GetTouch(i).position);
                if (hit.collider != null)
                {
                    hit.transform.GetComponent<Interactable>().endClick();
                    hit.transform.GetComponent<PlaySound>().PlayClip();
                }
            }
        }
    }
}
