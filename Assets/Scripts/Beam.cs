using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    public Rigidbody2D particle;
    public int angle;
    public int charge = 1;
    int counter = 0;
    public int electronCount = 0;
    public int fluxionCount = 0;
    public int gravitonCount = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 5)
        {
            spawn(particle, angle);
            counter = 0;
        }
        counter++;
    }


    void spawn(Rigidbody2D item, int angle)
    {
        Rigidbody2D clone;
        clone = Instantiate(item, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);

        Vector2 velocity = Quaternion.AngleAxis(UnityEditor.TransformUtils.GetInspectorRotation(transform).z + 90, Vector3.forward) * Vector3.right;
        velocity.Normalize();

        clone.AddForce(velocity * 400f, ForceMode2D.Impulse);
    }

}
