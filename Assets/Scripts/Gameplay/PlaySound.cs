using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip onHit;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip() {
        audioSource.clip = onHit;
        audioSource.Play();
    }

}
