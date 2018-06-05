using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    private System.Diagnostics.Stopwatch timer;
    private int i = 0;
    private float initialMillis;
    public int angle, charge;
    public Rigidbody2D particle, particleClone;
    public bool mReactGrav, mReactElec, mReactFlux, mBeamPositive;
    List<bool> mOut;
    public Sprite g, p, n, f, gp, gn, gf, pf, nf, gpf, gnf;
    private List<Sprite> sprites;
    private List<string> spriteChecker;
    private string grav, elec, flux;

    // Use this for initialization
    void Start()
    {
        sprites = new List<Sprite>();
        spriteChecker = new List<string>();

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

        //Remove this eventually
        //False charge attracts
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.ElapsedMilliseconds - initialMillis >= 250)
        {
            spawn(particle);
            initialMillis = timer.ElapsedMilliseconds;
        }

        setSprite();

    }

    void spawn(Rigidbody2D item)
    {
        particleClone = Instantiate(item, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        particleClone.GetComponent<Particle>().setProperties(getProperties());

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

    public void setSprite()
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

        string spriteSearcher = (grav + elec + flux);
        Sprite[] spriteArray = sprites.ToArray();
        string[] stringChecker = spriteChecker.ToArray();

        for(int i = 0; i < spriteArray.Length; i++)
        {
            if(stringChecker[i] == spriteSearcher)
            {
                this.GetComponent<SpriteRenderer>().sprite = spriteArray[i];
            }
        }
        
    }
}
