using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
//using Cinemachine.Examples;
public class water_script : MonoBehaviour
{
	public GameObject moon;
    public float far_height = 0.0f;
    public float close_height = 10.0f;
    public float move_speed = 100.0f;
	public float moon_height_when_close = 50.0f;
	public float moon_height_when_far = 100.0f;
    private AudioSource audioSource;
    public AudioClip deathSound;

    public GameObject TearOfAnger;
    public GameObject TearOfSorrow;
    public IceCrystal icecrystal;
    public FireCrystal firecrystal;
    public Transform insidefirepouch;
    public Transform insideicepouch;
    private FP_Playermovement player_movement_script;
    private Rigidbody rb;
    public bool firstblood = false;
    public NPC_script moonNPCscript;
    

    // Start is called before the first frame update
    void Start()
    {
        //player_movement_script = GameObject.FindGameObjectWithTag("Player").GetComponent<FP_Playermovement>();
		float desired_height = get_desired_height();
		Vector3 new_position = new Vector3(transform.position.x, desired_height, transform.position.z);
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        float desired_height = get_desired_height();
        float new_height = Mathf.MoveTowards(transform.position.y, desired_height, move_speed * Time.fixedDeltaTime);
        //float new_height = desired_height;

        Vector3 new_position = new Vector3(transform.position.x, new_height, transform.position.z);

        rb.MovePosition(new_position);
        //transform.position = new_position;
    }

	float get_desired_height() {
		float moon_height = moon.transform.position.y;
		float moon_height_range = moon_height_when_far - moon_height_when_close;

		float t = (moon_height - moon_height_when_close) / moon_height_range;

		return Mathf.Lerp(close_height, far_height, t);
	}
    
    void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("Player"))
        {
            if(firstblood == false)
            {
                moonNPCscript.firstdrown();
                firstblood = true;
                


            }
            GameObject NewFireCrystal = Instantiate(TearOfAnger, insidefirepouch.position, Quaternion.identity);
            GameObject NewIceCrystal = Instantiate(TearOfSorrow, insideicepouch.position, Quaternion.identity);
            Rigidbody rb1 = TearOfAnger.GetComponent<Rigidbody>();
            Rigidbody rb2 = TearOfSorrow.GetComponent<Rigidbody>();
            rb1.isKinematic = false;
            rb2.isKinematic = false;
            icecrystal = NewIceCrystal.gameObject.GetComponent<IceCrystal>();
            firecrystal = NewFireCrystal.gameObject.GetComponent<FireCrystal>();
            Invoke("ResetCrystals", 1f);
            

        }

       /* if (other.gameObject.CompareTag("FireCrystal"))
        {
            Instantiate(TearOfAnger, insidepouch.position, Quaternion.identity);
            Destroy(other.gameObject);
        } */
    }

    void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.CompareTag("FireCrystal"))
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

    public void ResetCrystals()
    {
        
        icecrystal.pickedup = false;
        firecrystal.pickedup = false;
    }

}
