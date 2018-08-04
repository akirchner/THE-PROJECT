using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamButton : MonoBehaviour {

	bool reactGrav, reactElec, reactFlux, beamPositive;
	int elecInt = 0;
	string grav, elec, flux;
	public Sprite g, p, n, f, gp, gn, gf, pf, nf, gpf, gnf;
    public Sprite[] sprites;
	List<bool> mOut;
	static string[] stringChecker = {"g", "p", "n", "f", "gp", "gn", "gf", "pf", "nf", "gpf", "gnf"};

	void start(){
        sprites = new Sprite[11];

        sprites[0] = g;
        sprites[1] = p;
        sprites[2] = g;
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

			reactElec = true;
			beamPositive = true;

		} 
		else if (elecInt % 3 == 1) {

			reactElec = true;
			beamPositive = false;

		} 
		else {
			
			reactElec = false;
			beamPositive = false;

		}

		SetSprite ();
	
	}

	public List<bool> GetProperties() {
		mOut.Clear();
		mOut.Add(reactGrav);
		mOut.Add(reactElec);
		mOut.Add(reactFlux);
		mOut.Add(beamPositive);

		return mOut;
	}

	public void GravClick(){
		if (reactGrav) {
			
			reactGrav = false;
		
		} 
		else {
		
			reactGrav = true;
		
		}
	}

	public void ElecClick(){

	}

	public void FluxClick(){
		if (reactFlux) {

			reactFlux = false;

		} 
		else {

			reactFlux = true;

		}
	}

	void SetSprite()
	{
		if (reactGrav)
		{
			grav = "g";
		}
		else
		{
			grav = "";
		}

		if (reactElec && beamPositive)
		{
			elec = "p";
		}
		else if (reactElec)
		{
			elec = "n";
		}
		else
		{
			elec = "";
		}

		if(reactFlux)
		{
			flux = "f";
		}
		else
		{
			flux = "";
		}

		string spriteSearcher = (grav + elec + flux);

		for(int i = 0; i < sprites.Length; i++)
		{
			if(stringChecker[i] == spriteSearcher)
			{
				this.GetComponent<Image> ().sprite = sprites[i];
			}
		}

	}
}
