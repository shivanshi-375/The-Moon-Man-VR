using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController2 : MonoBehaviour
{

    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private InputActionReference jumpActionReference2;
    [SerializeField] private InputActionReference pausebutton;

    public MoonToggle moon_control_script;

    public GameObject xRorigin;
    public Transform OriginTransform;
    public Transform CamTransform;
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
    public AudioClip stopmoon;
    public AudioClip menuon;
    public AudioClip menuoff;
    public GameObject Menu;
    public GameObject leftray;
    public GameObject rightray;


    public bool alreadyPlayed = false;
    public bool alreadyPlayed2 = false;
    public bool whirlpoolzone = false;
    public bool menutoggle = false;
    public Transform newmenuspot;

    public float Vol = 10f;
   

    public TMP_Text text;
    public TMP_Text tips;
    public Animator TipAnim;

    public string[] m_HardCodedStrings;


    private Rigidbody _body;

    public bool InMoonSpot;
    public bool moving_moon = false;
    private Vector3 moon_movement_start_point;
    private float moon_movement_start_t;
    public float full_move_controller_dist = 1.0f; //distance_needed_to_move_controllers_in_direction_of_moon_for_maximum_moon_movement
    public Transform moon_direction_transform;
    
    public Camera cam;
    public CapsuleCollider col;
    public Transform camOffset;
    public float ColliderMinHeight = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
      
        jumpActionReference.action.performed += OnJump;
        jumpActionReference.action.canceled += OnRelease;
        jumpActionReference2.action.performed += OnJump;
        jumpActionReference2.action.canceled += OnRelease;
        pausebutton.action.performed += OnPause;

        m_HardCodedStrings = new string[]
             {
                 "You got this! ",
                 "So Close! ",
                 "You're almost there!  ",
                 "Only a few more! ",
                 "You're getting better!",
             };
        cam = Camera.main;
        col = GetComponent<CapsuleCollider>();
        camOffset = transform.Find("Camera Offset");
    }

   
    void Update()
    {
        if (moving_moon)
        {
            do_moon_control();
        }
        else
        {
            //rightspell.SetActive(false);
            //leftspell.SetActive(false);
            audioSourceL.Stop();
            audioSourceR.Stop();
        }

        AdjustCollider();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 


    }

    void OnJump(InputAction.CallbackContext context)
    {

        
        if (InMoonSpot == true && menutoggle == false)
        {
            leftspell.SetActive(true);
            rightspell.SetActive(true);
            begin_moon_movement();

            /*if (!alreadyPlayed)
            {

                audioSourceL.clip = jetpacksound;
                audioSourceL.Play();
                audioSourceL.PlayOneShot(startupsound, 0.8f);
                alreadyPlayed = true;
                
            } */
        }
        
    }

    void OnRelease(InputAction.CallbackContext context)
    {
        leftspell.SetActive(false);
        rightspell.SetActive(false);
        //SpellOn = false;
        audioSourceL.Stop();
        GameAudio.PlayOneShot(stopmoon, 0.7F);
        alreadyPlayed = false;
        moving_moon = false;
    }

    void OnPause(InputAction.CallbackContext context)
    {
        if(menutoggle == false)
        {
            Menu.SetActive(true);
            Menu.transform.position = newmenuspot.transform.position;
            Menu.transform.LookAt(CamTransform);
            leftray.SetActive(true);
            rightray.SetActive(true);
            GameAudio.PlayOneShot(menuon, 0.7f);
            menutoggle = true;
        } else if(menutoggle == true)
        {

            Menu.SetActive(false);
            leftray.SetActive(false);
            rightray.SetActive(false);
            GameAudio.PlayOneShot(menuoff, 0.7f);
            menutoggle = false;
        }
    }


    public void RespawnStart()
    {
        xRorigin.transform.position = Respawn.position;
        moon_control_script.t = moon_control_script.starting_t;
        InMoonSpot = false;
        moving_moon = false;

        GetComponent<InWaterMoveProvider>().respawning = false;
    }

    private Vector3 get_moon_control_point()
    {
        Vector3 average_position = Vector3.Lerp(lefthand.position, righthand.position, 0.5f);
        Vector3 diff = average_position - OriginTransform.position;
        return diff;
    }

    public void begin_moon_movement()
    {

        if (moving_moon) return;
        moving_moon = true;

        moon_movement_start_point = get_moon_control_point();
        moon_movement_start_t = moon_control_script.t;
    }
    public void do_moon_control()
    {
        Vector3 control_point = get_moon_control_point();
        Vector3 delta = control_point - moon_movement_start_point;
        Vector3 direction_to_moon = -moon_direction_transform.forward;
        float distance_along_moon_axis = Vector3.Dot(delta, direction_to_moon);

        float goal_t = moon_movement_start_t + distance_along_moon_axis / full_move_controller_dist;
        moon_control_script.t += (goal_t - moon_control_script.t) * (1.0f - Mathf.Exp(-moon_control_script.speed * Time.deltaTime));
        moon_control_script.t = Mathf.Clamp(moon_control_script.t, 0.0f, 1.0f);
    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Water")
        {
            GetComponent<InWaterMoveProvider>().respawning = true;

            GameAudio.PlayOneShot(deathSound, 0.5f);
            
            Invoke("RespawnStart", 0.3f);
            Debug.Log("BLUBLUBLUB");
            tips.text = m_HardCodedStrings[Random.Range(0, m_HardCodedStrings.Length)];
            TipAnim.Play("Base Layer.TipCanvas", 0, 0);
        }

        if (col.collider.tag == "IcePlatform" && whirlpoolzone == true)
        {
            gameObject.transform.SetParent(col.transform, true);
        }

            if (col.collider.tag == "End")
        {
            Application.Quit();
        }


    }

    public void OnCollisionExit(Collision col)
    {

        if (col.collider.tag == "IcePlatform" && whirlpoolzone == true)
        {
            gameObject.transform.parent = null;
        }

    }

        public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Moonlitspot")
        {
            InMoonSpot = true;
        }

        if (other.GetComponent<Collider>().tag == "whirlpoolzone")
        {
            whirlpoolzone = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Moonlitspot")
        {
            InMoonSpot = false;
        }

        if (other.GetComponent<Collider>().tag == "whirlpoolzone")
        {
            whirlpoolzone = false;
        }
    }


    public void AdjustCollider()
    {
        float cameraYPos = cam.transform.position.y - camOffset.position.y + transform.position.y;
        float capsulHeight = col.height;
        //float newHeight = Mathf.Clamp((cameraYPos - center.y)*2, ColliderMinHeight, ColliderMaxHeight);
        float newHeight = Mathf.Max(ColliderMinHeight, cameraYPos - transform.position.y);
        
        col.height = newHeight;
        col.center = new Vector3(0f, newHeight / 2f, 0f);
    }




    //tips.text = "Congratulations, you have been accepted into Arcane Academy!";
    //TipAnim.Play("Base Layer.TipCanvas", 0, 0);


    //_body.velocity = OriginTransform.up * 200f * Time.fixedDeltaTime;






}