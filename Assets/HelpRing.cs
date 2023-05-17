using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpRing : MonoBehaviour
{

    public Animator ring1;
    public Animator ring2;
    public Animator ring3;
    public Animator ring4;
    public Animator ring5;
    public Animator ring6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Appear()
    {
        ring1.SetBool("Holding", true);
        ring2.SetBool("Holding", true);
        ring3.SetBool("Holding", true);
        ring4.SetBool("Holding", true);
        ring5.SetBool("Holding", true);
        ring6.SetBool("Holding", true);

    }

    public void Disappear()
    {

        ring1.SetBool("Holding", false);
        ring2.SetBool("Holding", false);
        ring3.SetBool("Holding", false);
        ring4.SetBool("Holding", false);
        ring5.SetBool("Holding", false);
        ring6.SetBool("Holding", false);
    }
}
