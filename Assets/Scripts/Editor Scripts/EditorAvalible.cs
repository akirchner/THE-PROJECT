using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorAvalible : MonoBehaviour {

	public Transform force;
	public bool isPositive;

	public void click(){
		if (isPositive) {
			force.GetComponent<ForceSpawner> ().numAvailable++;
		} 
		else {
			force.GetComponent<ForceSpawner> ().numAvailable--;
		}
	}
}
