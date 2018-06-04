using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    public Rigidbody2D particle;
    public int charge = 1;
    int counter = 0;
    public int electronCount;
    public int fluxionCount;
    public int gravitonCount;
    bool mReactGrav, mReactElec, mReactFlux;
    Charge mBeamCharge;
    List<int> mOut;

    public enum Charge
    {
        POSITIVE, 
        Neutral, 
        Negative
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 5)
        {
            spawn(particle);
            counter = 0;
        }
        counter++;
    }


    void spawn(Rigidbody2D item)
    {
        Rigidbody2D clone;
        clone = Instantiate(item, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);

        Vector2 velocity = Quaternion.AngleAxis(UnityEditor.TransformUtils.GetInspectorRotation(transform).z + 90, Vector3.forward) * Vector3.right;
        velocity.Normalize();

        clone.AddForce(velocity * 400f, ForceMode2D.Impulse);
    }

    public void setProperites(bool reactGrav, bool reactElec, bool reactFlux, Charge beamCharge) {
        mReactGrav = reactGrav;
        mReactElec = reactElec;
        mReactFlux = reactFlux;
        mBeamCharge = beamCharge;
    }

    public List<int> getProperties() {
        mOut = new List<int>();
        mOut.Add(mReactGrav ? 1 : 0);
        mOut.Add(mReactElec ? 1 : 0);
        mOut.Add(mReactFlux ? 1 : 0);

        return mOut;
    }

}
