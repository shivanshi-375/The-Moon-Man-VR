using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGet : MonoBehaviour
{

    public GameObject pouch;

    public AudioSource audio;

    public AudioClip clip;

    public IceCrystal icecrystal;
    public FireCrystal firecrystal;
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
        if (other.GetComponent<Collider>().tag == "pickaxe")
        {
            pouch.SetActive(true);
            audio.PlayOneShot(clip, 0.5f);
            Invoke("ResetCrystals", 2f);
        }
    }

    public void ResetCrystals()
    {
        icecrystal.pickedup = false;
        firecrystal.pickedup = false;
    }
  
}
