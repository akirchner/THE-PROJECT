using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Interactable {

    Transform other;

    new void Update()
    {

    }

    void OnTriggerExit2D (){
        if (other != null)
        {
            other.GetComponent<EditorInteractable>().isTrash = false;
        }
    }

	void OnTriggerEnter2D (Collider2D coll) {
		other = coll.transform;
        other.GetComponent<EditorInteractable>().isTrash = true;
	}
}
