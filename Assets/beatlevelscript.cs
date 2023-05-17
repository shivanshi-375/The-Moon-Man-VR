using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatlevelscript : MonoBehaviour
{
    public Animator plantanim;
    public GameObject icepouch;
    public GameObject firepouch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            icepouch.SetActive(true);
            firepouch.SetActive(true);
            plantanim.ResetTrigger("Open");
            plantanim.SetTrigger("Open");
        }
    }
}
