using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    List<float> gravDistanceX, gravDistanceY, elecDistanceX, elecDistanceY, fluxDistanceX, fluxDistanceY;
    private List<bool> mProperties;
    List<GameObject> mActiveForces;
    GameObject[] dragableF, staticF, dynamicF;
    float currentX, currentY;
    Vector2 gravForce, elecForce, fluxForce, resultant;
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
        mActiveForces = new List<GameObject>();
        gravForce = new Vector2();
        elecForce = new Vector2();
        fluxForce = new Vector2();
        resultant = new Vector2();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mActiveForces = GetComponentInParent<Beam>().GetActiveForces();
        currentX = transform.position.x;
        currentY = transform.position.y;

        foreach (GameObject i in mActiveForces)
        {
            try
            {
                switch (i.GetComponent<Properties>().type)
                {
                    case ForceType.Graviton:
                        gravDistanceX.Add((float)i.transform.position.x);
                        gravDistanceY.Add((float)i.transform.position.y);
                        break;
                    case ForceType.Electron:
                        elecDistanceX.Add((float)i.transform.position.x);
                        elecDistanceY.Add((float)i.transform.position.y);
                        break;
                    case ForceType.Fluxion:
                        fluxDistanceX.Add((float)i.transform.position.x);
                        fluxDistanceY.Add((float)i.transform.position.y);
                        break;
                    default:
                        break;
                }
            }
            catch(System.Exception)
            {
                switch (i.GetComponent<DynamicProperties>().production)
                {
                    case ForceType.Graviton:
                        gravDistanceX.Add((float)i.transform.position.x);
                        gravDistanceY.Add((float)i.transform.position.y);
                        break;
                    case ForceType.Electron:
                        elecDistanceX.Add((float)i.transform.position.x);
                        elecDistanceY.Add((float)i.transform.position.y);
                        break;
                    case ForceType.Fluxion:
                        fluxDistanceX.Add((float)i.transform.position.x);
                        fluxDistanceY.Add((float)i.transform.position.y);
                        break;
                    default:
                        break;
                }
            }
            
        }

        elecForce = Electrostatic(elecDistanceX, elecDistanceY, mProperties[1]);
        fluxForce = Flux(fluxDistanceX, fluxDistanceY, mProperties[2]);

        resultant = Gravity(gravDistanceX, gravDistanceY, mProperties[0]) + elecForce + fluxForce;

        rb.AddForce(resultant, ForceMode2D.Impulse);

        gravDistanceX.Clear();
        gravDistanceY.Clear();
        elecDistanceX.Clear();
        elecDistanceY.Clear();
        fluxDistanceX.Clear();
        fluxDistanceY.Clear();
    }

    private Vector2 Gravity(List<float> xDistance, List<float> yDistance, bool active) {
        float totalXForce = 0;
        float totalYForce = 0;
        float force;

        if (active)
        {
            for (int i = 0; i < xDistance.Count; i++)
            {
                Vector2 distance = new Vector2(currentX - xDistance[i], currentY - yDistance[i]);
                force = 200 / (Mathf.Pow(distance.magnitude, 2));

                if (currentX - xDistance[i] > 0)
                {
                    totalXForce -= force;
                }
                else
                {
                    totalXForce += force;
                }

                if (currentY - yDistance[i] > 0)
                {
                    totalYForce -= force;
                }
                else
                {
                    totalYForce += force;
                }
            }
        }

        gravForce.x = totalXForce;
        gravForce.y = totalYForce;

        return gravForce;
    }
    
    private Vector2 Electrostatic(List<float> xDistance, List<float> yDistance, bool active)
    {
        float totalXForce = 0;
        float totalYForce = 0;
        float force;

        if (active)
        {
            for (int i = 0; i < xDistance.Count; i++)
            {

                Vector2 distance = new Vector2(currentX - xDistance[i], currentY - yDistance[i]);
                force = 200 / Mathf.Pow(distance.magnitude, 2);

                if (mProperties[3])
                {
                    if (currentX - xDistance[i] > 0)
                    {
                        totalXForce -= force;
                    }
                    else
                    {
                        totalXForce += force;
                    }

                    if (currentY - yDistance[i] > 0)
                    {
                        totalYForce -= force;
                    }
                    else
                    {
                        totalYForce += force;
                    }
                }
                else
                {
                    if (currentX - xDistance[i] > 0)
                    {
                        totalXForce += force;
                    }
                    else
                    {
                        totalXForce -= force;
                    }

                    if (currentY - yDistance[i] > 0)
                    {
                        totalYForce += force;
                    }
                    else
                    {
                        totalYForce -= force;
                    }
                }
            }
        }

        elecForce.x = totalXForce;
        elecForce.y = totalYForce;

        return elecForce;
    }

    private Vector2 Flux(List<float> xDistance, List<float> yDistance, bool active)
    {
        float totalXForce = 0;
        float totalYForce = 0;
        float force;

        if (active)
        {
            for (int i = 0; i < xDistance.Count; i++)
            {

                Vector2 distance = new Vector2(currentX - xDistance[i], currentY - yDistance[i]);
                force = 200 / Mathf.Pow(distance.magnitude, 2);

                if (currentX - xDistance[i] > 0)
                {
                    totalXForce += force;
                }
                else
                {
                    totalXForce -= force;
                }

                if (currentY - yDistance[i] > 0)
                {
                    totalYForce += force;
                }
                else
                {
                    totalYForce -= force;
                }
            }
        }

        fluxForce.x = totalXForce;
        fluxForce.y = totalYForce;

        return fluxForce;
    }

    public void SetProperties(List<bool> beamProperties)
    {
        mProperties = beamProperties;
    }
}
