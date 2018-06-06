using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSave : MonoBehaviour {
    public int packID, levelID;
    public Transform check;

	// Use this for initialization
	void Start () {
		if (GameProperties.saveData[packID, levelID]) {
            Instantiate(check, new Vector3(this.GetComponent<CanvasRenderer>().transform.position.x, this.GetComponent<CanvasRenderer>().transform.position.y, 0), Quaternion.Euler(0, 0, 0));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
