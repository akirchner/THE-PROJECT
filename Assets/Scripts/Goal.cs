using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;


public class Goal : MonoBehaviour {

    int mParticleCount, mChange;
	// Use this for initialization
	void Start () {
        mChange = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (mParticleCount > 1000) {
            SceneManager.LoadScene("End");
        }
        if (mChange == 50) {
            Debug.Log("Losing Enegry!");
            mParticleCount--;
            mChange = 0;
        }
        mChange++;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Particle"))
        {
            Debug.Log("Particle has Enterd Goal! " + mParticleCount);
            Destroy(col.gameObject);
            mParticleCount++;
           }
    }
}
