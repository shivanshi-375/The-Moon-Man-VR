using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class RotateScript : MonoBehaviour
{
    public Transform StarCompass;
  
    [SerializeField]
    float rotateSpeed;

    [SerializeField]
    Vector3 rotationDirection = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);

        if(StarCompass.position.x > 0)
        {
            rotationDirection = new Vector3(0, 1, 0);
        } else if (StarCompass.position.x < 0)
        {
            rotationDirection = new Vector3(0, -1, 0);
        } else
        {
            rotationDirection = new Vector3(0, -1, 0);
        }
    }

   
}
