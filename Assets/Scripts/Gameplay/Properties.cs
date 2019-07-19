﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ForceType
{
    Graviton,
    Fluxion,
    Electron,
	Empty
}

public class Properties : Interactable
{
    public ForceType type;
    public Sprite gravSprite, elecSprite, fluxSprite;
    private string[] spriteSearcher;
    private Sprite[] sprites;

    // Use this for initialization
    void Start()
    {
        Beam.UpdateForces(gameObject, true);
        setType(type);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.CompareTag("StaticForce") || this.CompareTag("DynamicForce"))
        {
            //Disapproving noise goes here
        }
        else
        {        
            Movement();
        }
    }

	private void OnDestroy()
	{
        Beam.UpdateForces(gameObject, false);
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
