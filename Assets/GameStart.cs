using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    public Animator anim;
    public Animator titleanim;
    public GameObject Moon;
    public AudioSource audio;
    public AudioClip clip;
    public ParticleSystem part;
    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            Moon.SetActive(true);
            anim.ResetTrigger("FadeIn");
            anim.SetTrigger("FadeIn");
            titleanim.ResetTrigger("StartGame");
            titleanim.SetTrigger("StartGame");
            if (started == false)
            {
                audio.PlayOneShot(clip, 0.7f);
                part.Play();
                started = true;
            }
          
            
        }
    }

  
}
