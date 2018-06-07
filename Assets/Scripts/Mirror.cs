using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {
    int negater = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector2 newVelocity, currentVelocity;

        if(col.CompareTag("Mirror"))
        {
            currentVelocity = this.gameObject.GetComponent<Rigidbody2D>().velocity;
            int angle = angleFinder(col.transform.eulerAngles.z);

            switch (angle) {
                case 1:
                    newVelocity = new Vector2(-currentVelocity.x, currentVelocity.y);
                    break;
                case 2:
                    newVelocity = new Vector2(currentVelocity.x, -currentVelocity.y);
                    break;
                case 3:
                    newVelocity = new Vector2(currentVelocity.y * negater, -currentVelocity.x * negater);
                    break;
                default:
                    newVelocity = currentVelocity;
                    break;
            }

            this.gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity;
        }
    }

    private int angleFinder(float mirrorAngle)
    {
        if(mirrorAngle > 180)
        {
            negater = -1;
        }
        else
        {
            negater = 1;
        }

        if(Mathf.Abs(mirrorAngle) % 180 == 0)
        {
            return 1;
        }

        if(Mathf.Abs(mirrorAngle) % 90 == 0 || mirrorAngle == 0)
        {
            return 2;
        }

        if(Mathf.Approximately(Mathf.Abs(mirrorAngle), 45) || Mathf.Approximately(Mathf.Abs(mirrorAngle), 135) || Mathf.Approximately(Mathf.Abs(mirrorAngle), 225) || Mathf.Approximately(Mathf.Abs(mirrorAngle), 315))
        {
            return 3;
        }

        Debug.Log("Invalid mirror angle!");
        return 4;
    }
}
