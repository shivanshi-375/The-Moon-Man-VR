using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSail : MonoBehaviour
{

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Geyser")
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }

           
    }
}
