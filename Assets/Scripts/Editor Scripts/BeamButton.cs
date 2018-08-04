using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamButton : MonoBehaviour {

	bool reactGrav, reactElec, reactFlux, beamPositive;
	int elecInt = 0;
	string grav, elec, flux;
	public Sprite g, p, n, f, gp, gn, gf, pf, nf, gpf, gnf;
	List<bool> mOut;
	private List<Sprite> sprites;
	static string[] stringChecker = {"g", "p", "n", "f", "gp", "gn", "gf", "pf", "nf", "gpf", "gnf"};

	void start(){
		sprites = new List<Sprite>();

		sprites.Add(g);
		sprites.Add(p);
		sprites.Add(n);
		sprites.Add(f);
		sprites.Add(gp);
		sprites.Add(gn);
		sprites.Add(gf);
		sprites.Add(pf);
		sprites.Add(nf);
		sprites.Add(gpf);
		sprites.Add(gnf);

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

		for(int i = 0; i < 9; i++)
		{
			if(stringChecker[i] == spriteSearcher)
			{
				this.GetComponent<Image> ().sprite = sprites[i];
			}
		}

	}
}
