using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour {
    public int id;
    private int lastID;
    private Color color;

	// Use this for initialization
	void Start () {
        setColor();
	}
	
	// Update is called once per frame
	void Update () {
        if(id != lastID)
        {
            setColor();
        }

        this.gameObject.transform.Rotate(new Vector3(0, 0, -1), Space.Self);
        lastID = id;
	}

    private void setColor()
    {
        switch (id)
        {
            case 0:
                color = Color.red;
                break;
            case 1:
                color = Color.blue;
                break;
            case 2:
                color = Color.green;
                break;
            case 3:
                color = Color.magenta;
                break;
            case 4:
                color = Color.yellow;
                break;
            case 5:
                color = Color.cyan;
                break;
            case 6:
                color = Color.grey;
                break;
            case 7:
                color = Color.white;
                break;
            default:
                Debug.Log("What is my identity?");
                break;
        }

        GetComponent<SpriteRenderer>().color = color;
    }
}
