using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrystal : MonoBehaviour
{
    public GameObject TearOfAnger;
    public Transform insidepouch;
    public int inventory;
    public Animator anim;
    public FireCrystal script;
    public GameObject NewFireCrystal;

    public AudioSource audio;
    public AudioClip clip;
    public GameObject geyser_prefab;
    public bool pickedup = false;
    public bool emptypouch = false;
    // Start is called before the first frame update
    void Start()
    {
        inventory = 4;
        
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Geyser") && pickedup == true)
        {
            pickedup = false;
            Debug.Log("Geyser Geyser!");
            anim = other.transform.gameObject.GetComponent<Animator>();
            anim.ResetTrigger("Start");
            anim.SetTrigger("Start");
            NewFireCrystal = Instantiate(TearOfAnger, insidepouch.position, Quaternion.identity);
            NewFireCrystal.transform.position = insidepouch.position;
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Water") && pickedup == true)
        {
            Debug.Log("Water Geyser!");
            Debug.Log(transform.position);
            GameObject geyser = Instantiate(geyser_prefab, transform.position, Quaternion.identity);

            anim = geyser.transform.gameObject.GetComponent<Animator>();
            anim.ResetTrigger("Start");
            anim.SetTrigger("Start");
            NewFireCrystal = Instantiate(TearOfAnger, insidepouch.position, Quaternion.identity);
            //Instantiate(TearOfAnger, insidepouch.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void returntoinventory()
    {
        inventory++;
    }

    public void aftergrab()
    {
        if(inventory > 0 && emptypouch == true)
        {
            inventory--;
            emptypouch = false;
            NewFireCrystal = Instantiate(TearOfAnger, insidepouch.position, Quaternion.identity);
            script = NewFireCrystal.transform.gameObject.GetComponent<FireCrystal>();
            script.inventory = inventory--;
            script.emptypouch = false;
            
        }
        
    }

    public void pickup()
    {
        audio.PlayOneShot(clip);
        pickedup = true;
    }

    public void empty()
    {
        emptypouch = true;
    }
}
