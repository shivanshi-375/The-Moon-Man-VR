using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessFix : MonoBehaviour
{
    public Camera cam;
    public bool fixme = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
       
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("fix", 3f);
    }

    public void fix()
    {
        if(fixme == false){
            cam.useOcclusionCulling = true;
            //cam.useOcclusionCulling = false;
            fixme = true;
        }
    }
}
