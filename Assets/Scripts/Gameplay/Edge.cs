using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private GameObject[] wormholes;
    private bool canBeTeleported;
    private System.Diagnostics.Stopwatch timer;
    private long initialMillis;
    // Use this for initialization
    void Start()
    {
        timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        canBeTeleported = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer.ElapsedMilliseconds - initialMillis >= 35)
        {
            canBeTeleported = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Wormhole"))
        {
            try
            {
                int searchID = col.GetComponent<Wormhole>().id;
                Vector3 safetyPosition = col.transform.position;

                for (int i = 0; i < wormholes.Length; i++)
                {
                    if (wormholes[i].GetComponent<Wormhole>().id == searchID && wormholes[i].transform.position != safetyPosition)
                    {
                        if (canBeTeleported)
                        {
                            Vector3 relativeDisplacement = new Vector3(this.gameObject.transform.position.x - col.transform.position.x, this.gameObject.transform.position.y - col.transform.position.y, 0);
                            this.gameObject.transform.position = wormholes[i].transform.position;
                            this.gameObject.transform.position += relativeDisplacement;
                            canBeTeleported = false;
                            initialMillis = timer.ElapsedMilliseconds;
                        }
                    }
                }
            } catch(System.NullReferenceException e)
            {
                
            }
            
        }
        else
        {
            if (!col.CompareTag("Goal") && !col.CompareTag("Mirror"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
