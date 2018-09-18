using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

    private System.Diagnostics.Stopwatch timer;
    private float initialMillis, periodicity;
    public Rigidbody2D particle, particleClone;
    public bool mReactGrav, mReactElec, mReactFlux, mBeamPositive;
    List<bool> mOut;
    public Sprite g, p, n, f, gp, gn, gf, pf, nf, gpf, gnf;
    private List<Sprite> sprites;
    private string grav, elec, flux, spriteSearcher;
    private Vector2 velocity;
    public static readonly List<GameObject> mForces = new List<GameObject>();
    static string[] stringChecker = {"g", "p", "n", "f", "gp", "gn", "gf", "pf", "nf", "gpf", "gnf"};

    // Use this for initialization
    void Start()
    {
        sprites = new List<Sprite>();
        mOut = new List<bool>();

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

        timer = new System.Diagnostics.Stopwatch();
        timer.Start();
        initialMillis = timer.ElapsedMilliseconds;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameProperties.bigfalconbeam)
        {
            periodicity = 6;
        }
        else
        {
            periodicity = 60;
        }

        if (timer.ElapsedMilliseconds - initialMillis >= periodicity)
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
        if (mReactGrav)
        {
            grav = "g";
        }
        else
        {
            grav = "";
        }

        if (mReactElec && mBeamPositive)
        {
            elec = "p";
        }
        else if (mReactElec)
        {
            elec = "n";
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

        for(int i = 0; i < sprites.Count; i++)
        {
            if(stringChecker[i] == spriteSearcher)
            {
                this.GetComponent<SpriteRenderer>().sprite = sprites[i];
            }
        }
        
    }

    public static void UpdateForces(GameObject gameObject, bool active)
    {
        if (active)
        {
            mForces.Add(gameObject);
        }
        else 
        {
            mForces.Remove(gameObject);
        }
	}

    public List<GameObject> GetActiveForces() 
    {
        return mForces;
    }
}
