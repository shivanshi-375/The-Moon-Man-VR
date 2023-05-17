using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtYou : MonoBehaviour
{

    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(cam.transform.position);
    }
}
