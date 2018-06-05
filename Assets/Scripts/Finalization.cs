using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;


public class Finalization : MonoBehaviour {
    
    public int targetCount;
    Vector3 mStep;
    int mParticleCount, mDecrementDelay;
    public Transform ProgressBar;

	// Use this for initialization
	void Start () {
        mDecrementDelay = 35;
        mStep = new Vector3((18f / targetCount), 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (mParticleCount > targetCount) {
            Initiate.Fade("End", Color.black, 4f);
        }
        if (mDecrementDelay <= 0 && mParticleCount != 0) {
            mParticleCount--;
            ProgressBar.localScale -= mStep;
            mDecrementDelay = 2 * 50/targetCount;
        }
        mDecrementDelay--;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Particle"))
        {
            Destroy(col.gameObject);
            mParticleCount++;
            ProgressBar.localScale += mStep;
            mDecrementDelay = 35;
        }
    }
}

