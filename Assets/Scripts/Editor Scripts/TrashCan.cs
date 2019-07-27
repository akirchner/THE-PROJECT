using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour {

	Transform trash;

	void Update(){
		if (Input.GetMouseButtonUp (0)) {
			if(trash != null) {
				if (trash.parent != null) {
					Destroy (trash.parent.gameObject);
				} 
				else {
                    if(trash.gameObject.CompareTag("Wormhole"))
                    {
                        int id = trash.gameObject.GetComponent<Wormhole>().id;
                        GameObject[] wormholes = GameObject.FindGameObjectsWithTag("Wormhole");

                        for(int i = 0; i < wormholes.Length; i++)
                        {
                            if(wormholes[i].GetComponent<Wormhole>().id == id)
                            {
                                Destroy(wormholes[i]);
                            }
                        }

                        GameObject.Find("Wormhole").GetComponent<EditorSpawner>().setAvailableID(id, true);

                    }
                    else
                    {
                        Destroy(trash.gameObject);
                    }
                }
			}
		}
	}

	void OnTriggerExit2D (){
		trash = null;
	}

	void OnTriggerEnter2D (Collider2D coll) {
		Transform temp = coll.transform;
		if (temp.GetComponent<DragAndDrop> ().isDragged) {
			trash = temp;
		}
	}
}
