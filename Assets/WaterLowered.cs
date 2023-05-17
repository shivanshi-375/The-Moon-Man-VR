using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLowered : MonoBehaviour
{
    
    public NPC_script moonNPCscript;
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
        if (other.gameObject.CompareTag("Water"))
        {
            moonNPCscript.firstw = true;
            moonNPCscript.firstwater();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            moonNPCscript.firstw = false;
        }

    }
}
