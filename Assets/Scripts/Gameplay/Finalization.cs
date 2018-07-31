using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;
            
public class Finalization : MonoBehaviour
{

	public bool isTutorial = false;
	public int targetCount;
    public Transform ProgressBar;
    int mParticleCount, mDecrementDelay;
    bool mFull;
    List<bool> mEnd;
    Vector3 mStep;
    GameObject[] mGoals;



	// Use this for initialization
	void Start () {
        mDecrementDelay = 35;
        mStep = new Vector3((18f / targetCount), 0, 0);
        mFull = false;
        mEnd = new List<bool>();
        mGoals = GameObject.FindGameObjectsWithTag("Goal");
    }
	
	// Update is called once per frame
	void Update () {

        foreach (GameObject i in mGoals) {
			
            mEnd.Add(i.GetComponent<Finalization>().isFull());
        
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

        updateCount(false);
        mDecrementDelay--;
       	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Particle"))
        {
            Destroy(col.gameObject);
            updateCount(true);
        }
    }

    public bool isFull() {
        return mFull;
    }

    void updateCount(bool increase) {
        if (increase && mParticleCount <= targetCount)
        {
            mParticleCount++;
            ProgressBar.localScale += mStep;
            mDecrementDelay = 35;
        }
        else if (mDecrementDelay <= 0 && mParticleCount != 0)
        {
            mParticleCount--;
            ProgressBar.localScale -= mStep;
            mDecrementDelay = 2;
        }
        mFull = (mParticleCount >= targetCount) ? true : false;
    }

}

