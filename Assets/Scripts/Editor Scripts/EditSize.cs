using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditSize : MonoBehaviour {

	public float scaleFactor = 2;
	public Transform top, bottom;
	Vector3 topPos = new Vector3();
	Vector3 botPos = new Vector3();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		topPos = top.transform.position;
		botPos = bottom.transform.position;
		Apply (Midpoint (topPos, botPos), Rotation(topPos, botPos), Magnitude(topPos, botPos));
	}

	float Rotation(Vector3 a, Vector3 b){
		float angle = Vector3.SignedAngle(new Vector3(1,0,0),new Vector3((a.x-b.x),(a.y-b.y),0),new Vector3(0,0,1)) ;
		return angle;
	}

	float Magnitude(Vector3 a, Vector3 b){
		float magnitude = Vector3.Magnitude(new Vector3((a.x - b.x),(a.y - b.y),0));
		return magnitude*scaleFactor;
	}

	Vector3 Midpoint(Vector3 a, Vector3 b){
		float newX = (a.x + b.x) / 2;
		float newY = (a.y + b.y) / 2;
		return new Vector3 (newX, newY, 0);
	}

	void Apply(Vector3 position, float rotation, float scale){
		this.transform.position = position;
		this.transform.rotation = Quaternion.Euler(0, 0, (float)rotation);
		this.transform.localScale = new Vector3((float) scale, 1, 1);
	}
}
