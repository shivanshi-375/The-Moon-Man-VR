using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserScript : MonoBehaviour
{
    Animator anim = null;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo state_info = anim.GetCurrentAnimatorStateInfo(0);
        if (state_info.normalizedTime >= 1.0)
        { 
            //Debug.Log("Destroy!1234");
            Destroy(this.gameObject);
        }
    }
}
