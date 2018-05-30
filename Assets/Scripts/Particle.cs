using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    List<float> distanceX, distanceY, size;
    float totalXForce, totalYForce;
    string forceID;
    float currentX, currentY;
    string type = "unknown";
    public float gravityConstant = 1;
    public Rigidbody2D rb;
    public Vector2 inVel = new Vector2(1, 1);


    // Use this for initialization
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        distanceX = new List<float>();
        distanceY = new List<float>();
        size = new List<float>();
        rb.velocity = inVel;
    }

    // Update is called once per frame
    void Update()
    {

        string searchString;
        currentX = this.transform.position.x;
        currentY = this.transform.position.y;

        for (int i = 0; i < GameObject.Find("Beam").GetComponent<Beam>().gravitonCount; i++)
        {
            if (i == 0)
            {
                searchString = "Graviton";
            }
            else
            {
                searchString = "Graviton (" + i + ")";
            }

            distanceX.Add((float) GameObject.Find(searchString).transform.position.x);
            distanceY.Add((float) GameObject.Find(searchString).transform.position.y);
            size.Add(GameObject.Find(searchString).GetComponent<Properties>().size);
            
        }

        Gravity(distanceX.ToArray(), distanceY.ToArray(), size.ToArray());
        distanceX.Clear();
        distanceY.Clear();

    }

    private void Gravity(float[] xDistance, float[] yDistance, float[] size) {
    
        for(int i = 0; i < xDistance.Length; i++)
        {
            if(currentX - xDistance[i] > 0)
            {
                totalXForce -= (size[i] * gravityConstant) / (Mathf.Pow((currentX - xDistance[i]), 2));
            }
            else
            {
                totalXForce += (size[i] * gravityConstant) / (Mathf.Pow((currentX - xDistance[i]), 2));
            }

            if (currentY - yDistance[i] > 0)
            {
                totalYForce -= (size[i] * gravityConstant) / (Mathf.Pow((currentY - yDistance[i]), 2));
            }
            else
            {
                totalYForce += (size[i] * gravityConstant) / (Mathf.Pow((currentY - yDistance[i]), 2));
            }
        }

        Vector2 resultant = new Vector2(totalXForce, totalYForce);
        //Debug.Log("Resultant yForce: " + totalYForce);
        rb.AddForce(resultant, ForceMode2D.Impulse);
        totalXForce = 0;
        totalYForce = 0;
    }
}
