using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private InputActionReference jumpActionReference2;

    public MoonToggle script;

    public GameObject xRorigin;
    public Transform OriginTransform;
    public Transform Respawn;
    public Vector3 SpellObj;
    public Transform lefthand;
    public Transform righthand;
    public GameObject leftspell;
    public GameObject rightspell;
    public GameObject manaHUD;
    public AudioSource audioSourceL;
    public AudioSource audioSourceR;
    public AudioSource GameAudio;
    public AudioClip jetpacksound;
    public AudioClip startupsound;
    public AudioClip deathSound;
    public bool SpellOn;
    public bool SpellOn2;
    public bool InMoonSpot;

    public bool alreadyPlayed = false;
    public bool alreadyPlayed2 = false;

    public float Vol = 10f;
   

    public TMP_Text text;
    public TMP_Text tips;
    public Animator TipAnim;

    public string[] m_HardCodedStrings;


    private Rigidbody _body;


    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
      
        jumpActionReference.action.performed += OnJump;
        jumpActionReference.action.canceled += OnRelease;
        jumpActionReference2.action.performed += OnJump2;
        jumpActionReference2.action.canceled += OnRelease2;
    


        m_HardCodedStrings = new string[]
             {
                 "You got this! ",
                 "So Close! ",
                 "You're almost there!  ",
                 "Only a few more! ",
                 "You're getting better!",
             };

    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (SpellOn == true && InMoonSpot == true )
        {
            Spell();
        } else if (SpellOn2 == true && InMoonSpot == true)
        {
            Spell2();
        }else {
            //rightspell.SetActive(false);
            //leftspell.SetActive(false);
            audioSourceL.Stop();
            audioSourceR.Stop();
        }


    }

    void OnJump(InputAction.CallbackContext context)
    {
        if(InMoonSpot == true)
        {
            leftspell.SetActive(true);
            SpellOn = true;
            

            /*if (!alreadyPlayed)
            {

                audioSourceL.clip = jetpacksound;
                audioSourceL.Play();
                audioSourceL.PlayOneShot(startupsound, 0.8f);
                alreadyPlayed = true;
                Debug.Log("brrrr");
            } */
        }
        
    }

    void OnRelease(InputAction.CallbackContext context)
    {
        leftspell.SetActive(false);
        SpellOn = false;
        audioSourceL.Stop();
        alreadyPlayed = false;
        

    }

    void OnJump2(InputAction.CallbackContext context)
    {
        if (InMoonSpot == true)
        {
            rightspell.SetActive(true);
            SpellOn2 = true;
            

            if (!alreadyPlayed2)
            {

                /*audioSourceR.clip = jetpacksound;
                audioSourceR.Play();
                audioSourceR.PlayOneShot(startupsound, 0.8f);
                alreadyPlayed2 = true;
                Debug.Log("brrrr");*/
            }
        }
    }

    void OnRelease2(InputAction.CallbackContext context)
    {
        rightspell.SetActive(false);
        SpellOn2 = false;
        audioSourceR.Stop();
        alreadyPlayed2 = false;
       
    }


   

    void Spell()
    {

        script.t += script.speed * Time.deltaTime;
        //_body.velocity = (righthand.up * -1) + (lefthand.up * -1) * jumpForce * Time.fixedDeltaTime;
    }

    void Spell2()
    {

        script.t -= script.speed * Time.deltaTime;

    }
    public void RespawnStart()
    {
        xRorigin.transform.position = Respawn.position;
    }
    
    

    public void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Water")
        {


            GameAudio.PlayOneShot(deathSound, 0.5f);
            
            Invoke("RespawnStart", 0.1f);
            Debug.Log("BLUBLUBLUB");
            tips.text = m_HardCodedStrings[Random.Range(0, m_HardCodedStrings.Length)];
            TipAnim.Play("Base Layer.TipCanvas", 0, 0);
        }

        if(col.collider.tag == "End")
        {
            Application.Quit();
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Moonlitspot")
        {
            InMoonSpot = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Moonlitspot")
        {
            InMoonSpot = false;
        }
    }







        //tips.text = "Congratulations, you have been accepted into Arcane Academy!";
        //TipAnim.Play("Base Layer.TipCanvas", 0, 0);


        //_body.velocity = OriginTransform.up * 200f * Time.fixedDeltaTime;






    }