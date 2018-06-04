using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Properties : MonoBehaviour
{

    public string type;
    public float size;
    public string movementType;
    bool click = false;

    // Use this for initialization
    void Start()
    {
        setSize(size);
        setSprite();

        switch (type.ToUpper())
        {
            case "GRAVITON":
                GameObject.Find("Beam").GetComponent<Beam>().setGravitonCount(GameObject.Find("Beam").GetComponent<Beam>().gravitonCount + 1);
                break;
            case "ELECTRON":
                GameObject.Find("Beam").GetComponent<Beam>().setElectronCount(GameObject.Find("Beam").GetComponent<Beam>().electronCount + 1);
                break;
            case "FLUXION":
                GameObject.Find("Beam").GetComponent<Beam>().setFluxionCount(GameObject.Find("Beam").GetComponent<Beam>().fluxionCount + 1);
                break;
            default:
                Debug.Log("Invalid type! Type: " + type.ToUpper());
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void setSize(float newSize)
    {
        GetComponent<Transform>().localScale = new Vector3(newSize, newSize, newSize);
    }

    public void setSprite()
    {

    }

}
