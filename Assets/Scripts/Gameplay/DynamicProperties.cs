using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ReactType
{
    Gravity,
    Positive,
    Negative,
    Flux
}

public class DynamicProperties : MonoBehaviour
{
    

    public ForceType production;
    public ReactType reaction;
    public bool isEditor = false;
    public Sprite ef, eg, fg, fn, fp, gf, gn, gp;
    private string[] spriteSearcher;
    private Sprite[] sprites;
    private string produces, reacts;

    // Use this for initialization
    void Start()
    {
        if(!isEditor)
        {
            Beam.UpdateForces(gameObject, true);
        }

        spriteSearcher = new string[8];
        sprites = new Sprite[8];

        sprites[0] = ef;
        sprites[1] = eg;
        sprites[2] = fg;
        sprites[3] = fn;
        sprites[4] = fp;
        sprites[5] = gf;
        sprites[6] = gn;
        sprites[7] = gp;

        for (int i = 0; i < sprites.Length; i++)
        {
            spriteSearcher[i] = sprites[i].ToString();
        }

        setSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if(isEditor)
        {
            setSprite();
        }
    }

    private void OnDestroy()
    {
        if(isEditor)
        {
            Beam.UpdateForces(gameObject, false);
        }
    }

    private void setSprite()
    {
 

        switch (reaction.ToString())
        {
            case "Gravity":
                reacts = "g";
                break;
            case "Positive":
                reacts = "p";
                break;
            case "Negative":
                reacts = "n";
                break;
            case "Flux":
                reacts = "f";
                break;
            default:
                Debug.Log("Invalid ReactType in Properties.cs");
                break;
        }

        switch (production.ToString())
        {
            case "Graviton":
                produces = "g";
                break;
            case "Electron":
                produces = "e";
                break;
            case "Fluxion":
                produces = "f";
                break;
            default:
                Debug.Log("Invalid ProductionType in Properties.cs");
                break;
        }

        for (int i = 0; i < spriteSearcher.Length; i++)
        {
            if (produces + reacts == spriteSearcher[i].Substring(0, 2).ToLower())
            {
				if (isEditor) {
					this.GetComponent<Image> ().sprite = sprites [i];
				} 
				else {
					this.GetComponent<SpriteRenderer> ().sprite = sprites [i];
				}
			}
        }
    }

    public ForceType getType()
    {
        return this.production;
    }

}
