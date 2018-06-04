using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour {

    int mParticleCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Particle"))
        {
            Debug.Log("Particle has Enterd Goal!");
            Destroy(col.gameObject);
            mParticleCount++;
        }
    }
}
