using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public enum Location
    {
        GRAV,
        ELEC,
        FLUX,
        MIXED,
        WORMHOLE,
        DYNAMIC,
        EDITOR,
        BLANK,
    }

    private bool fadingIn, fadingOut;
    private int whenAreWe = 0;
    public Location currentLocation = Location.GRAV;
    private Location previousLocation = Location.GRAV;
    private AudioSource source;
    public AudioClip[] music = new AudioClip[14];

    private static Music instance = null;
    public static Music Instance { get { return instance; } }

    // Use this for initialization
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        source = GameObject.Find("Music").GetComponent<AudioSource>();
        source.clip = music[0];
        source.Play();
        source.loop = false;
        fadingIn = false;
        fadingOut = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentLocation != previousLocation)
        {
            fadingOut = true;
        }

        bool fading = fadingIn || fadingOut;

        if (fading)
        {
            if (fadingOut)
            {
                if (whenAreWe > 120)
                {
                    whenAreWe = 0;
                    source.loop = false;

                    switch (currentLocation)
                    {
                        case Location.GRAV:
                            source.clip = music[0];
                            break;
                        case Location.ELEC:
                            source.clip = music[2];
                            break;
                        case Location.FLUX:
                            source.clip = music[4];
                            break;
                        case Location.MIXED:
                            source.clip = music[6];
                            break;
                        case Location.WORMHOLE:
                            source.clip = music[8];
                            break;
                        case Location.DYNAMIC:
                            source.clip = music[10];
                            break;
                        case Location.EDITOR:
                            source.clip = music[12];
                            break;
                        default:
                            Debug.Log("No known musical location! " + currentLocation);
                            break;
                    }

                    source.Play();
                    fadingOut = false;
                    fadingIn = true;
                }
                else
                {
                    source.volume -= (float)(1.0 / 120);
                    whenAreWe++;
                }
            }
            else
            {
                if (whenAreWe > 120)
                {
                    whenAreWe = 0;
                    fadingIn = false;
                }
                else
                {
                    source.volume += (float)(1.0 / 120);
                    whenAreWe++;
                }
            }
        }

        if (!source.isPlaying)
        {
            switch (currentLocation)
            {
                case Location.GRAV:
                    source.clip = music[1];
                    break;
                case Location.ELEC:
                    source.clip = music[3];
                    break;
                case Location.FLUX:
                    source.clip = music[5];
                    break;
                case Location.MIXED:
                    source.clip = music[7];
                    break;
                case Location.WORMHOLE:
                    source.clip = music[9];
                    break;
                case Location.DYNAMIC:
                    source.clip = music[11];
                    break;
                case Location.EDITOR:
                    source.clip = music[13];
                    break;
                default:
                    Debug.Log("No known musical location! " + currentLocation);
                    break;
            }

            source.loop = true;
            source.Play();
        }

        previousLocation = currentLocation;
    }

    public void SetLocation(int location)
    {
        switch (location) {
            case 1:
                currentLocation = Location.GRAV;
                break;
            case 2:
                currentLocation = Location.ELEC;
                break;
            case 3:
                currentLocation = Location.FLUX;
                break;
            case 4:
                currentLocation = Location.MIXED;
                break;
            case 5:
                currentLocation = Location.WORMHOLE;
                break;
            case 6:
                currentLocation = Location.DYNAMIC;
                break;
            case 7:
                currentLocation = Location.EDITOR;
                break;
            default:
                Debug.Log("Invalid setlocation for music! " + location);
                break;
        }
    }
}
