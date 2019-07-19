using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditRotation : Interactable {

	public Transform target;
    private float rotation;
	float x,y;
	// Update is called once per frame
	new void Update () {
        Movement();
		Apply (Rotation (target.position));
	}

	float Rotation(Vector3 a){
		x = this.GetComponent<Transform>().position.x;
		y = this.GetComponent<Transform>().position.y;
		float angle = Vector3.SignedAngle (new Vector3 (1, 0, 0), new Vector3 ((a.x-x), (a.y-y), 0), new Vector3 (0, 0, 1));
		return angle;
	}

	void Apply(float rotation){
        this.transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
        this.rotation = rotation + 90;
	}

    public float getRotation()
    {
        return rotation;
    }
}
