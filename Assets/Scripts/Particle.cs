﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    List<float> gravDistanceX, gravDistanceY, elecDistanceX, elecDistanceY, fluxDistanceX, fluxDistanceY, mass, charge, fluxcapacity;
    GameObject[] allObjects;
    float currentX, currentY;
    string type = "unknown";
    public float gravityConstant = 1;
    public float electricConstant = 1;
    public float fluxConstant = 1;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gravDistanceX = new List<float>();
        gravDistanceY = new List<float>();
        elecDistanceX = new List<float>();
        elecDistanceY = new List<float>();
        fluxDistanceX = new List<float>();
        fluxDistanceY = new List<float>();
        mass = new List<float>();
        charge = new List<float>();
        fluxcapacity = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        currentX = this.transform.position.x;
        currentY = this.transform.position.y;
        float[] gravForce, elecForce, fluxForce;
        gravForce = new float[2];
        elecForce = new float[2];
        fluxForce = new float[2];
       
        float resultantXForce = 0;
        float resultantYForce = 0;

        for (int i = 0; i < allObjects.Length; i++)
        {
            try {
                switch (allObjects[i].GetComponent<Properties>().type)
                {
                    case ForceType.Graviton:
                        gravDistanceX.Add((float)allObjects[i].transform.position.x);
                        gravDistanceY.Add((float)allObjects[i].transform.position.y);
                        mass.Add((float)allObjects[i].GetComponent<Properties>().size);
                        break;
                    case ForceType.Electron:
                        elecDistanceX.Add((float)allObjects[i].transform.position.x);
                        elecDistanceY.Add((float)allObjects[i].transform.position.y);
                        charge.Add((float)allObjects[i].GetComponent<Properties>().size);
                        break;
                    case ForceType.Fluxion:
                        fluxDistanceX.Add((float)allObjects[i].transform.position.x);
                        fluxDistanceY.Add((float)allObjects[i].transform.position.y);
                        fluxcapacity.Add((float)allObjects[i].GetComponent<Properties>().size);
                        break;
                    default:
                        break;
                }
            }
            catch(System.Exception e)
            {
                Debug.Log(e.StackTrace);
            }            
        }

        gravForce = gravity(gravDistanceX.ToArray(), gravDistanceY.ToArray(), mass.ToArray());
        elecForce = electrostatic(elecDistanceX.ToArray(), elecDistanceY.ToArray(), charge.ToArray());
        fluxForce = flux(fluxDistanceX.ToArray(), fluxDistanceY.ToArray(), fluxcapacity.ToArray());

        //Check against particle type here

        resultantXForce = (gravForce[0] + elecForce[0] + fluxForce[0]);
        resultantYForce = (gravForce[1] + elecForce[1] + fluxForce[1]);
        
        Vector2 resultant = new Vector2(resultantXForce, resultantYForce);
        rb.AddForce(resultant, ForceMode2D.Impulse);
        gravDistanceX.Clear();
        gravDistanceY.Clear();
        mass.Clear();
        elecDistanceX.Clear();
        elecDistanceY.Clear();
        charge.Clear();
        fluxDistanceX.Clear();
        fluxDistanceY.Clear();
        fluxcapacity.Clear();
    }

    private float[] gravity(float[] xDistance, float[] yDistance, float[] mass) {
        float totalXForce = 0;
        float totalYForce = 0;
    
        for(int i = 0; i < xDistance.Length; i++)
        {
            Vector2 distance = new Vector2(currentX - xDistance[i], currentY - yDistance[i]);

            if(currentX - xDistance[i] > 0)
            {
                totalXForce -= (mass[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
            }
            else
            {
                totalXForce += (mass[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
            }

            if (currentY - yDistance[i] > 0)
            {
                totalYForce -= (mass[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
            }
            else
            {
                totalYForce += (mass[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
            }
        }

        float[] gravForce = new float[2];
        gravForce[0] = totalXForce;
        gravForce[1] = totalYForce;

        return gravForce;
    }

    private float[] electrostatic(float[] xDistance, float[] yDistance, float[] charge)
    {
        float totalXForce = 0;
        float totalYForce = 0;

        for (int i = 0; i < xDistance.Length; i++)
        {

            Vector2 distance = new Vector2(currentX - xDistance[i], currentY - yDistance[i]);

            if (GameObject.Find("Beam").GetComponent<Beam>().charge > 0)
            {
                if (currentX - xDistance[i] > 0)
                {
                    totalXForce += (charge[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
                }
                else
                {
                    totalXForce -= (charge[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
                }

                if (currentY - yDistance[i] > 0)
                {
                    totalYForce += (charge[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
                }
                else
                {
                    totalYForce -= (charge[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
                }
            }
            else
            {
                if (currentX - xDistance[i] > 0)
                {
                    totalXForce -= (charge[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
                }
                else
                {
                    totalXForce += (charge[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
                }

                if (currentY - yDistance[i] > 0)
                {
                    totalYForce -= (charge[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
                }
                else
                {
                    totalYForce += (charge[i] * gravityConstant) / (Mathf.Pow(distance.magnitude, 2));
                }
            }
        }

        float[] elecForce = new float[2];
        elecForce[0] = totalXForce;
        elecForce[1] = totalYForce;

        return elecForce;
    }

    private float[] flux(float[] xDistance, float[] yDistance, float[] fluxcapacity)
    {
        float totalXForce = 0;
        float totalYForce = 0;

        for (int i = 0; i < xDistance.Length; i++)
        {

            Vector2 distance = new Vector2(currentX - xDistance[i], currentY - yDistance[i]);

            if (currentX - xDistance[i] > 0)
            {
                totalXForce += (fluxcapacity[i] * fluxConstant) / (Mathf.Pow(distance.magnitude, 2));
            }
            else
            {
                totalXForce -= (fluxcapacity[i] * fluxConstant) / (Mathf.Pow(distance.magnitude, 2));
            }

            if (currentY - yDistance[i] > 0)
            {
                totalYForce += (fluxcapacity[i] * fluxConstant) / (Mathf.Pow(distance.magnitude, 2));
            }
            else
            {
                totalYForce -= (fluxcapacity[i] * fluxConstant) / (Mathf.Pow(distance.magnitude, 2));
            }
        }

        float[] fluxForce = new float[2];
        fluxForce[0] = totalXForce;
        fluxForce[1] = totalYForce;

        return fluxForce;
    }
}
