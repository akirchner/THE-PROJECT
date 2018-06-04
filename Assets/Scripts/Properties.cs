using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ForceType
{
    Graviton,
    Fluxion,
    Electron,
	Empty

}

public class Properties : MonoBehaviour
{

    public ForceType type;
    public float size;
    public string movementType;
	public Sprite gravSprite;
	public Sprite elecSprite;
	public Sprite fluxSprite;

    // Use this for initialization
    void Start()
    {
        
		setSize(size);
		
    }

    // Update is called once per frame
    void Update()
    {
		setType (type);
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
			this.GetComponent<SpriteRenderer> ().sprite = gravSprite;
            break;
        case ForceType.Fluxion:
			this.GetComponent<SpriteRenderer> ().sprite = fluxSprite;
            break;
        case ForceType.Electron:
			this.GetComponent<SpriteRenderer> ().sprite = elecSprite;
            break;
        }
    }

    public ForceType getType()
    {
        return this.type;
    }

}
