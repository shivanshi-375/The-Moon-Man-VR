using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSounds : MonoBehaviour
{

    public AudioSource audio;

    public AudioClip whaleup;
    public AudioClip whaledown;
    public AudioClip whalesound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   

    public void splashup()
    {
        audio.PlayOneShot(whaleup, 0.3f);
    }

    public void splashdown()
    {
        audio.PlayOneShot(whaledown, 0.7f);
    }

    public void whaaale()
    {
        audio.PlayOneShot(whalesound, 0.7f);
    }
}
