using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
//using Cinemachine.Examples;
public class water_script1 : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip deathSound;

    public GameObject TearOfSorrow;
    public GameObject TearOfAnger;
    public Transform insideicepouch;
    public Transform insidefirepouch;
    private FP_Playermovement player_movement_script;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //player_movement_script = GameObject.FindGameObjectWithTag("Player").GetComponent<FP_Playermovement>();
		
        audioSource = GetComponent<AudioSource>();
       
    }

    
    void FixedUpdate()
    {
        
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            Instantiate(TearOfAnger, insidefirepouch.position, Quaternion.identity);
            Instantiate(TearOfSorrow, insideicepouch.position, Quaternion.identity);
            Rigidbody rb1 = TearOfAnger.GetComponent<Rigidbody>();
            Rigidbody rb2 = TearOfSorrow.GetComponent<Rigidbody>();
            rb1.isKinematic = false;
            rb2.isKinematic = false;


        }

    }

    void OnCollisionEnter(Collision collision)
    {
        /* if (collision.gameObject.CompareTag("FireCrystal"))
         {
             Instantiate(TearOfAnger, insidefirepouch.position, Quaternion.identity);
             Destroy(collision.gameObject);
         }

         if (collision.gameObject.CompareTag("IceCrystal"))
         {
             Instantiate(TearOfSorrow, insideicepouch.position, Quaternion.identity);
             Destroy(collision.gameObject);
         } */
    }



}
