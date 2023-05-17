using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveCollider : MonoBehaviour
{

    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

    

    // Update is called once per frame
    

    public void collideron()
    {
        if(rb.isKinematic == false)
        {
            GetComponent<MeshCollider>().isTrigger = false;
        }
        
    }

    public void collideroff()
    {
        rb.isKinematic = false;
        if (rb.isKinematic == false)
        {
            GetComponent<MeshCollider>().isTrigger = true;
        }
    }


   
}
