using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    public Rigidbody2D particle;
    private System.Diagnostics.Stopwatch timer;
    private float initialMillis;
    public int angle;
    public int charge = 1;
    int counter = 0;
    public int electronCount = 0;
    public int fluxionCount = 0;
    public int gravitonCount = 0;

    public enum Charge
    {
        POSITIVE, Neutral, Negative
    }

    // Use this for initialization
    void Start()
    {
        timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        initialMillis = timer.ElapsedMilliseconds;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.ElapsedMilliseconds - initialMillis >= 120)
        {
            spawn(particle, angle);
            initialMillis = timer.ElapsedMilliseconds;
        }

    }


    void spawn(Rigidbody2D item, int angle)
    {
        Rigidbody2D clone;
        clone = Instantiate(item, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);

        Vector2 velocity = Quaternion.AngleAxis(UnityEditor.TransformUtils.GetInspectorRotation(transform).z + 90, Vector3.forward) * Vector3.right;
        velocity.Normalize();

        clone.AddForce(velocity * 400f, ForceMode2D.Impulse);
    }

    public void setProperites(bool reactGrav, bool reactElec, bool reactFlux, Charge charge) {
        
    }

}
