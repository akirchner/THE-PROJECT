using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    private System.Diagnostics.Stopwatch timer;
    private float initialMillis;
    public int angle;
    public Rigidbody2D particle, particleClone;
    public int charge, electronCount, fluxionCount, gravitonCount;
    int counter = 0;
    public bool mReactGrav, mReactElec, mReactFlux, mBeamPositive;
    List<bool> mOut;

    // Use this for initialization
    void Start()
    {
        timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        initialMillis = timer.ElapsedMilliseconds;

        setProperites(true, true, true, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.ElapsedMilliseconds - initialMillis >= 120)
        {
            spawn(particle);
            initialMillis = timer.ElapsedMilliseconds;
        }

        //Remove this eventually
        //False charge attracts
    }


    void spawn(Rigidbody2D item)
    {
        particleClone = Instantiate(item, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);

        Vector2 velocity = Quaternion.AngleAxis(UnityEditor.TransformUtils.GetInspectorRotation(transform).z + 90, Vector3.forward) * Vector3.right;
        velocity.Normalize();

        particleClone.AddForce(velocity * 400f, ForceMode2D.Impulse);
    }

    public void setProperites(bool reactGrav, bool reactElec, bool reactFlux, bool beamPositive) {
        mReactGrav = reactGrav;
        mReactElec = reactElec;
        mReactFlux = reactFlux;
        mBeamPositive = beamPositive;
    }

    public List<bool> getProperties() {
        mOut = new List<bool>();
        mOut.Add(mReactGrav);
        mOut.Add(mReactElec);
        mOut.Add(mReactFlux);
        mOut.Add(mBeamPositive);

        return mOut;
    }

}
