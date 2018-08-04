using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour {

	Transform trash;

	void OnTriggerEnter (Collider coll) {
		Debug.Log ("Bonk");
		Transform temp = coll.transform;
		if (temp.GetComponent<DragAndDrop> ().isDragged) {
			trash = temp;
		}
	}

	void OnMouseUp () {
		Debug.Log ("click");
		Destroy (trash.gameObject);
	}
}
