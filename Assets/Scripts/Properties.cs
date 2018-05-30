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
        try {
            this.GetComponent<SpriteRenderer>().sprite = (Resources.Load(type.ToUpper() + "_" + movementType.ToUpper(), typeof(Sprite)) as Sprite);
        }
        catch(UnassignedReferenceException e)
        {

        }
    }

}
