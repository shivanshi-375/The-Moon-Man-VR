using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxRandomizer : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource source;
    [Range(0.1f, 0.5f)]
    public float volChangeFactor = 0.2f;
    [Range(0.1f, 0.5f)]
    public float pitchChangeFactor = 0.2f;

    [Range(1, 1999)]
    public int soundTriggerProbability = 1000;

    public bool allowOverlap;

    public bool randomVol;
    public bool randomPitch = true;

    // Start is called before the first frame update
    public int randomPlayID = 1;
    public int randomPlayMax;
    void Start()
    {
        source = GetComponent<AudioSource>();
        randomPlayMax = 2000 - soundTriggerProbability;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, randomPlayMax) == randomPlayID) {
            if (!allowOverlap) {
                if (!source.isPlaying) {
                    source.clip = sounds[Random.Range(0, sounds.Length)];
                    if (randomVol) {
                        source.volume = Random.Range(1 - volChangeFactor, 1);
                    }
                    if (randomPitch) {
                        source.pitch = Random.Range(1 - pitchChangeFactor, 1 + pitchChangeFactor);
                    }
                    source.PlayOneShot(source.clip);
                }
            } else {
                source.clip = sounds[Random.Range(0, sounds.Length)];
                if (randomVol) {
                    source.volume = Random.Range(1 - volChangeFactor, 1);
                }
                if (randomPitch) {
                    source.pitch = Random.Range(1 - pitchChangeFactor, 1 + pitchChangeFactor);
                }
                source.PlayOneShot(source.clip);
            }
        }
        // if (Input.GetKeyDown(KeyCode.S)) {
        //     source.clip = sounds[Random.Range(0, sounds.Length)];
        //     source.volume = Random.Range(1 - volChangeFactor, 1);
        //     source.pitch = Random.Range(1 - pitchChangeFactor, 1 + pitchChangeFactor);
        //     source.PlayOneShot(source.clip);
        // }
    }
}
