using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    private System.Diagnostics.Stopwatch timer;
    private float initialMillis;
    public int angle, charge;
    public Rigidbody2D particle, particleClone;
    public bool mReactGrav, mReactElec, mReactFlux, mBeamPositive;
    GameObject[] dragableF, staticF, dynamicF;
    List<bool> mOut;
    public Sprite g, p, n, f, gp, gn, gf, pf, nf, gpf, gnf;
    private List<Sprite> sprites;
    private List<string> spriteChecker;
    private string grav, elec, flux, spriteSearcher;
    private Vector2 velocity;
    private List<GameObject[]> mForces;
    Sprite[] spriteArray;
    string[] stringChecker; // = {"g", "p", "n", "f", "gp", "gn", "gf", "pf", "nf", "gpf", "gnf"};

    // Use this for initialization
    void Start()
    {
        sprites = new List<Sprite>();
        spriteChecker = new List<string>();
        mOut = new List<bool>();
        mForces = new List<GameObject[]>();

        sprites.Add(g);
        sprites.Add(p);
        sprites.Add(n);
        sprites.Add(f);
        sprites.Add(gp);
        sprites.Add(gn);
        sprites.Add(gf);
        sprites.Add(pf);
        sprites.Add(nf);
        sprites.Add(gpf);
        sprites.Add(gnf);

        spriteChecker.Add("g");
        spriteChecker.Add("p");
        spriteChecker.Add("n");
        spriteChecker.Add("f");
        spriteChecker.Add("gp");
        spriteChecker.Add("gn");
        spriteChecker.Add("gf");
        spriteChecker.Add("pf");
        spriteChecker.Add("nf");
        spriteChecker.Add("gpf");
        spriteChecker.Add("gnf");

        timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        initialMillis = timer.ElapsedMilliseconds;
        UpdateForces();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer.ElapsedMilliseconds - initialMillis >= 60)
        {
            Spawn(particle);
            initialMillis = timer.ElapsedMilliseconds;
        }

        SetSprite();
    }

    void Spawn(Rigidbody2D item)
    {
        velocity = Quaternion.AngleAxis(this.transform.eulerAngles.z, Vector3.forward) * Vector2.up;
        velocity.Normalize();

        particleClone = Instantiate(item, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        particleClone.GetComponent<Particle>().SetProperties(GetProperties());
        particleClone.transform.SetParent(transform);

        particleClone.AddForce(velocity * 400f, ForceMode2D.Impulse);
    }

    public void SetProperites(bool reactGrav, bool reactElec, bool reactFlux, bool beamPositive) {
        mReactGrav = reactGrav;
        mReactElec = reactElec;
        mReactFlux = reactFlux;
        mBeamPositive = beamPositive;
    }

    public List<bool> GetProperties() {
        mOut.Clear();
        mOut.Add(mReactGrav);
        mOut.Add(mReactElec);
        mOut.Add(mReactFlux);
        mOut.Add(mBeamPositive);

        return mOut;
    }

    public void SetSprite()
    {
        if(mReactGrav)
        {
            grav = "g";
        }
        else
        {
            grav = "";
        }

        if(mReactElec)
        {
            if(mBeamPositive)
            {
                elec = "p";
            }
            else
            {
                elec = "n";
            }
        }
        else
        {
            elec = "";
        }

        if(mReactFlux)
        {
            flux = "f";
        }
        else
        {
            flux = "";
        }

        spriteSearcher = (grav + elec + flux);
        spriteArray = sprites.ToArray();
        stringChecker = spriteChecker.ToArray();

        for(int i = 0; i < spriteArray.Length; i++)
        {
            if(stringChecker[i] == spriteSearcher)
            {
                this.GetComponent<SpriteRenderer>().sprite = spriteArray[i];
            }
        }
        
    }

    public void UpdateForces() 
    {
        mForces.Clear();
        mForces.Add(GameObject.FindGameObjectsWithTag("DragableForce"));
        mForces.Add(GameObject.FindGameObjectsWithTag("StaticForce"));
        mForces.Add(GameObject.FindGameObjectsWithTag("DynamicForce"));
	}

    public List<GameObject[]> GetActiveForces() 
    {
        return mForces;
    }
}
