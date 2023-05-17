using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBreakTrigger : MonoBehaviour
{
    public GameObject[] floor_to_break;
    public Rigidbody[] floor_rigidbody;

    

    // Start is called before the first frame update
    void Start()
    {
        floor_to_break = GameObject.FindGameObjectsWithTag("Cells");
        floor_rigidbody = new Rigidbody[floor_to_break.Length];


        for (int i = 0; i < floor_to_break.Length; i++)
        {
            GameObject cell = floor_to_break[i];
            floor_rigidbody[i] = cell.GetComponent<Rigidbody>();

            
           
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Rigidbody rb in floor_rigidbody)
            {
                rb.useGravity = true;
                rb.GetComponent<MeshCollider>().isTrigger = true;
                rb.isKinematic = false;
            }
        }
        

    }

     
}
