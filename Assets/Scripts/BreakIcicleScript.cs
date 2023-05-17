using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIcicleScript : MonoBehaviour
{
    public Rigidbody rb;
    public int steps = 3;
    public bool stepped = false;
    public bool firststep = false;
    public bool twostep = false;
    public bool laststep = false;
    public Renderer meshRenderer;
    public Material material;
    public AudioSource audio;
    public AudioClip crackclip;
    public AudioClip breakclip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        steps = 3;
        stepped = false;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        
    }

    // Update is called once per frame
    void Update()
    {
        breaking();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && stepped == false || collision.gameObject.CompareTag("PlayerHand") && stepped == false)
        {
            stepped = true;
            steps--;
            Debug.Log(steps);
            Debug.Log(stepped);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && stepped == true || stepped == true)
        {
            stepped = false;
            Debug.Log(stepped);

        }
    }

    public void breaking()
    {
        if (steps == 2 && firststep == false)
        {
            audio.PlayOneShot(crackclip);
            material.SetFloat("_Metallic", 0.5f);
            firststep = true;
            Debug.Log(steps);
        }

        else if (steps == 1 && twostep == false)
        {
            audio.PlayOneShot(crackclip);
            material.SetFloat("_Metallic", 1f);
            twostep = true;
            Debug.Log(steps);
        }

        else if (steps == 0 && laststep == false)
        {
            audio.PlayOneShot(breakclip);
            fallnow();
            Destroy(gameObject, 3f);
            laststep = true;
            Debug.Log(steps);
        }
    }

    public void fallnow()
    {
        GetComponent<BoxCollider>().enabled = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
    }
}
