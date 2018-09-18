using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSecret : MonoBehaviour {

	public void toggleSecret()
    {
        if(GameProperties.bigfalconbeam)
        {
            GameProperties.bigfalconbeam = false;
        }
        else
        {
            GameProperties.bigfalconbeam = true;
        }
    }
}
