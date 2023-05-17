using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public bool is_shrunk = false;


    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       
        if (is_shrunk)
        {

            anim.SetBool("lightOn", true);

        } else
        {
            anim.SetBool("lightOn", false);
        }
            
            
     

       
    }


    public void grow()
    {
//        if (!is_shrunk) return;

        is_shrunk = false;
    }

    public void shrink()
    {
//        if (is_shrunk) return;

        is_shrunk = true;
    }
}
