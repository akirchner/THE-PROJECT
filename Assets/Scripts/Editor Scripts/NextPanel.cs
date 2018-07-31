using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPanel : MonoBehaviour {

	public Transform thisPanel, nextPanel;

	public void click(){
		thisPanel.GetComponent<ClosePannel>().Close ();
		nextPanel.GetComponent<ClosePannel>().Close ();
	}
}
