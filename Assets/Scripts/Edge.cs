using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        

        if (col.CompareTag("edge") || !col.CompareTag("Goal"))
        {
            Debug.Log("Paticle has Found edge!");
            Destroy(this.gameObject);
        }
    }
}
