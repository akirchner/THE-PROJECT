using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Mirror")) {

            Vector2 currentVel = this.gameObject.GetComponent<Rigidbody2D>().velocity;
            float mirrorAngle = col.transform.eulerAngles.z;
            
            Vector2 normal = (Vector2)(Quaternion.Euler(0, 0, mirrorAngle) * Vector2.right);

            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.Reflect(currentVel, normal);
        }
    }
}
