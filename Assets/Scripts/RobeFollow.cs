using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeFollow : MonoBehaviour
{

    public Transform Camera;
    public Transform target;
    public float height;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowHead();

    }

    public void FollowHead()
    {
        Vector3 campos = Camera.transform.position;
        transform.position = new Vector3(campos.x, transform.position.y, campos.z);
        //transform.LookAt(target);
    }


}
