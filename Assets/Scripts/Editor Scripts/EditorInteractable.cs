using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorInteractable : Interactable
{

    public bool isTrash = false;
    Transform self;

    void Start()
    {
        self = GetComponent<Transform>().transform;
    }

    // Update is called once per frame
    new void Update()
    {
        Movement();
    }

    new public void endClick()
    {
        Debug.Log("endclick");
        if (isTrash)
        {
            if (self.parent != null)
            {
                Destroy(self.parent.gameObject);
            }
            else
            {
                if (self.gameObject.CompareTag("Wormhole"))
                {
                    int id = self.gameObject.GetComponent<Wormhole>().id;
                    GameObject[] wormholes = GameObject.FindGameObjectsWithTag("Wormhole");

                    for (int i = 0; i < wormholes.Length; i++)
                    {
                        if (wormholes[i].GetComponent<Wormhole>().id == id)
                        {
                            Destroy(wormholes[i]);
                        }
                    }

                    GameObject.Find("Wormhole").GetComponent<EditorSpawner>().setAvailableID(id, true);
                }
                else
                {
                    Destroy(self.gameObject);
                }
            }
        }
        else
        {
            isDragged = false;
            touchID = -1;
        }
    }
}
