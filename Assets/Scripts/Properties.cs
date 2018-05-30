using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Properties : MonoBehaviour
{

    public string type;
    public float size;
    public bool isMobile;
    bool click = false;

    // Use this for initialization
    void Start()
    {
      
        setSize(size);

        switch (type.ToUpper())
        {
            case "GRAVITON":
			GameObject.Find("Beam").GetComponent<Beam>().setGravitonCount(GameObject.Find("Beam").GetComponent<Beam>().gravitonCount + 1);
                break;
            case "ELECTRON":
			GameObject.Find("Beam").GetComponent<Beam>().setElectronCount(GameObject.Find("Beam").GetComponent<Beam>().electronCount + 1);
                break;
            default:
                Debug.Log("Invalid particle type! Type: " + type);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        Vector2 r = new Vector2();
        Camera c = Camera.main;
        Event e = Event.current;
        Vector2 mousePos = new Vector2();

        Vector2 current = new Vector2(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y);

        //Get the mouse position from Event.
        //Note that the y position from Event is inverted.
        mousePos.x = e.mousePosition.x;
        mousePos.y = c.pixelHeight - e.mousePosition.y;

        r = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y));


        if (click)
        {
            GetComponent<Transform>().position = r;
        }
    }



    void setSize(float newSize)
    {
        GetComponent<Transform>().localScale = new Vector3(newSize, newSize, newSize);
    }

    Color setColor(string type)
    {
        switch (type)
        {
            case "graviton":
                return Color.green;
            case "electron":
                return Color.yellow;
            case "fluxion":
                return Color.magenta;

        }
        return Color.gray;
    }

}
