using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamButton : MonoBehaviour {

	public Transform beam, close;
	bool reactGrav, reactElec, reactFlux, beamPositive;
	int elecInt = 0;
	string grav, elec, flux;
	public Sprite g, p, n, f, gp, gn, gf, pf, nf, gpf, gnf;
    public Sprite[] sprites;
	List<bool> mOut;
	static string[] stringChecker = {"g", "p", "n", "f", "gp", "gn", "gf", "pf", "nf", "gpf", "gnf"};

	void Start () {
        sprites = new Sprite[11];

        sprites[0] = g;
        sprites[1] = p;
        sprites[2] = n;
        sprites[3] = f;
        sprites[4] = gp;
        sprites[5] = gn;
        sprites[6] = gf;
        sprites[7] = pf;
        sprites[8] = nf;
        sprites[9] = gpf;
        sprites[10] = gnf;

	}

	// Update is called once per frame
	void Update () {
		if (elecInt % 3 == 0) {

			elec = "p";

		} 
		else if (elecInt % 3 == 1) {

			elec = "n";

		} 
		else {
			
			elec = "";

		}

		SetSprite ();
	
	}

	public void create(){
		Transform temp;
		temp = Instantiate (beam, new Vector3(0,0,0), Quaternion.identity);
		temp.GetChild(0).GetComponent<Beam>().SetProperites (reactGrav, reactElec, reactFlux, beamPositive);
		close.GetComponent<ClosePannel> ().Close();
	}

	public void GravClick(){
		if (grav == "g") {
			
			grav = "";
		
		} 
		else {
		
			grav = "g";
		
		}
	}

	public void ElecClick(){
		elecInt++;
	}

	public void FluxClick(){
		if (flux == "f") {

			flux = "";

		} 
		else {

			flux = "f";

		}
	}

	void SetSprite()
	{
		
		string spriteSearcher = (grav + elec + flux);
		if(spriteSearcher != ""){
			if (grav == "g") {
				reactGrav = true;
			} 
			else {
				reactGrav = false;
			}
			if (elec == "p") {
				reactElec = true;
				beamPositive = true;
			} 
			else if(elec == "n") {
				reactElec = true;
				beamPositive = false;
			}
			else {
				reactElec = false;
				beamPositive = false;
			}
			if (flux == "f") {
				reactFlux = true;
			} 
			else {
				reactFlux = false;
			}
		} 


		for(int i = 0; i < sprites.Length; i++)
		{
			if(stringChecker[i] == spriteSearcher)
			{
				this.GetComponent<Image> ().sprite = sprites[i];
			}
		}

	}
}
