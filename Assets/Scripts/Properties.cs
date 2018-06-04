using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ForceType
{
    Graviton,
    Fluxion,
    Electron
}

public class Properties : MonoBehaviour
{

    public ForceType type;
    public float size;
    public string movementType;
    bool click = false;

    // Use this for initialization
    void Start()
    {
        setSize(size);
        setSprite();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void setSize(float newSize)
    {
        GetComponent<Transform>().localScale = new Vector3(newSize, newSize, newSize);
    }

    public void setType(ForceType type)
    {
        this.type = type;

        switch(type) {
        case ForceType.Graviton:
            setSprite(); //TODO put actual sprite here
            break;
        case ForceType.Fluxion:
            setSprite(); //TODO put actual sprite here
            break;
        case ForceType.Electron:
            setSprite(); //TODO put actual sprite here
            break;
        }
    }

    public ForceType getType()
    {
        return this.type;
    }

    public void setSprite()
    {

    }

}
