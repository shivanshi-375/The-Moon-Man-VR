using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureScript : MonoBehaviour
{
    public bool is_shrunk = false;

    public GameObject moon;
    public Animator transformanim;
    public Animator anim;
    public Animator moonanim;
    public NPC_Creature moontalkstart;
    public bool starttalk = false;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
       
            
            
     

       
    }


    public void grow()
    {
//        if (!is_shrunk) return;

        is_shrunk = false;
    }

    public void shrink()
    {
        if (this.gameObject.CompareTag("firelevel"))
        {
            gm.complete_fire_level();
        } else if (this.gameObject.CompareTag("icelevel"))
        {
            gm.complete_ice_level();
        }
        //        if (is_shrunk) return;
        moon.SetActive(true);
        anim.ResetTrigger("lightOn");
        anim.SetTrigger("lightOn");
        transformanim.ResetTrigger("Begin");
        transformanim.SetTrigger("Begin");
        moonanim.ResetTrigger("FadeIn");
        moonanim.SetTrigger("FadeIn");
        if(starttalk == false)
        {
            moontalkstart.NPC_dialogueoption = 2;
            moontalkstart.NPC_dialogueindex = 0;
            moontalkstart.Interact();
            
            starttalk = true;
        }

        is_shrunk = true;
    }
}
