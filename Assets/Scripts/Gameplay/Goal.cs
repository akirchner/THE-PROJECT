using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;

public class Goal : MonoBehaviour {

	public bool isTutorial = false;
	bool bfb; //shhhhh. . .
	int bfbCount = 0;
	public Transform ProgressBar;
	int mParticleCount = 0;
	int mDecrementDelay = 35;
	bool mFull = false;
	List<bool> mEnd;
	GameObject[] mGoals;

	void Start() {

		bfb = GameProperties.bigfalconbeam;

		mEnd = new List<bool>();
		mGoals = GameObject.FindGameObjectsWithTag("Goal"); // finds all goals in the scene

	}

	void Update() {
		//checks each goal for completion status
		foreach (GameObject i in mGoals) { 

			mEnd.Add(i.GetComponent<Goal>().isFull());

		}

		if (mEnd.TrueForAll(x => x)) {

			if (isTutorial) {
				Initiate.Fade ("TutorialEnd", Color.black, 4f);
			} 
			else {
				Initiate.Fade ("End", Color.black, 4f);
			}
		} 
		else {
			mEnd.Clear();
		}

		updateCount (false);

		//sets the size of the green bar that fills the goal graphically
		if(mParticleCount >= 0 && mParticleCount <= 50) {
			ProgressBar.localScale = new Vector3 (mParticleCount * (18f / 50f), 1, 0);
		}
	}

	//activated when a particle collides with the goal
	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag ("Particle")) {
			Destroy (col.gameObject);
			if (bfb && bfbCount >= 5) {
				updateCount (true);
				bfbCount = 0;
			} 
			else if (bfb) {
				bfbCount++;
				mDecrementDelay = 35;
			} 
			else {
				updateCount (true);
			}
		}
	}

	//runs (false) evry frame unless OnTrigger was called that frame in which case it is called (true)
	void updateCount(bool isIncreaseing) {
		if (isIncreaseing) {
			
			mParticleCount++;
			mDecrementDelay = 35;
			mFull = (mParticleCount >= 50) ? true : false;
				
		} 
		else {
			if (mDecrementDelay < 0 && mParticleCount > 0 ) {
				mParticleCount--;
				mDecrementDelay = 2;
			}
			mDecrementDelay--;
		}
	}


	//allows each instance of a goal to get the other's completion status
	public bool isFull() {
		return mFull;
	}

}
