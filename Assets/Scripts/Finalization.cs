using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;


public class Finalization : MonoBehaviour {

    int mParticleCount, mChange;
    public int targetCount;
	// Use this for initialization
	void Start () {
        mChange = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (mParticleCount > targetCount) {
            SceneManager.LoadScene("End");
        }
        if (mChange == 0 && mParticleCount != 0) {
            mParticleCount--;
            Debug.Log("Losing Enegry! " + mParticleCount);
            mChange = 5;
        }
        mChange--;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Particle"))
        {
            Destroy(col.gameObject);
            Debug.Log(col.gameObject.GetComponent<Transform>().position.x);
            mChange = 35;
           }
    }
}
