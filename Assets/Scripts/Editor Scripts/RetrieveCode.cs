using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetrieveCode : MonoBehaviour {
    InputField field;

	// Use this for initialization
	void Start () {
        field = GameObject.Find("InputField").GetComponent<InputField>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCode()
    {
        GameProperties.levelcode = field.text;
    }
}
