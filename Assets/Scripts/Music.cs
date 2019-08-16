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
    public AudioSource[] sources = new AudioSource[2];

    private int fadeTimer = 120;
    private float maxVolume = 0.5f;

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

        sources[0].clip = music[0];
        sources[1].clip = music[1];
        sources[0].Play();
        sources[1].PlayScheduled(AudioSettings.dspTime + 25.6);
        sources[0].volume = maxVolume;
        sources[1].volume = maxVolume;
        
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
                if (whenAreWe > fadeTimer)
                {
                    whenAreWe = 0;
                    sources[1].Stop();
                
                    switch (currentLocation)
                    {
                        case Location.GRAV:
                            sources[0].clip = music[0];
                            sources[1].clip = music[1];
                            break;
                        case Location.ELEC:
                            sources[0].clip = music[2];
                            sources[1].clip = music[3];
                            break;
                        case Location.FLUX:
                            sources[0].clip = music[4];
                            sources[1].clip = music[5];
                            break;
                        case Location.MIXED:
                            sources[0].clip = music[6];
                            sources[1].clip = music[7];
                            break;
                        case Location.WORMHOLE:
                            sources[0].clip = music[8];
                            sources[1].clip = music[9];
                            break;
                        case Location.DYNAMIC:
                            sources[0].clip = music[10];
                            sources[1].clip = music[11];
                            break;
                        case Location.EDITOR:
                            sources[0].clip = music[12];
                            sources[1].clip = music[13];
                            break;
                        default:
                            Debug.Log("No known musical location! " + currentLocation);
                            break;
                    }

                    sources[0].Play();
                    sources[1].PlayScheduled(AudioSettings.dspTime + 25.6);
                    fadingOut = false;
                    fadingIn = true;
                }
                else
                {
                    sources[0].volume -= (float)(maxVolume / fadeTimer);
                    sources[1].volume -= (float)(maxVolume / fadeTimer);
                    whenAreWe++;
                }
            }
            else
            {
                if (whenAreWe > fadeTimer)
                {
                    whenAreWe = 0;
                    fadingIn = false;
                }
                else
                {
                    sources[0].volume += (float)(.5/ fadeTimer);
                    sources[1].volume += (float)(.5/ fadeTimer);
                    whenAreWe++;
                }
            }
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
