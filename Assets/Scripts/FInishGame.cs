using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FInishGame : MonoBehaviour
{
    //public AudioSource audio;
    //public AudioClip clip;

    public GameObject moon;
    public Animator moonanim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            moon.SetActive(true);
            moonanim.ResetTrigger("FadeIn");
            moonanim.SetTrigger("FadeIn");


            Debug.Log("You Win!");
            
        }
    }

}
