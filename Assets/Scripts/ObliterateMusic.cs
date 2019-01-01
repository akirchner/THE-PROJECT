using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObliterateMusic : MonoBehaviour {

    private static ObliterateMusic instance = null;

    public static ObliterateMusic Instance
    {
        get { return instance; }
    }

    // Use this for initialization
    void Start () {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
