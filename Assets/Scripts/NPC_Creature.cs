using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC_Creature : MonoBehaviour
{
    public string NPC_name = "Default Name";
    public List<string> NPC_dialogue = new List<string>();
    public List<string> NPC_dialogue2 = new List<string>();
    public List<string> NPC_dialogue3 = new List<string>();
    public List<string> NPC_dialogue4 = new List<string>();
    
    //public List<AudioClip> voicelist = new List<AudioClip>();

    public GameObject NPC_panel;
    public TMPro.TextMeshProUGUI NPC_text;
    public TMPro.TextMeshProUGUI NPC_name_text;
    private bool NPC_talking = false;
    public int NPC_dialogueoption = 1;
    public int NPC_dialogueindex = 0;
    
    

    public AudioSource audio;
    public AudioClip clip;

    [SerializeField] private InputActionReference buttonref;

    public Animator moonanim;
    public Animator moonmananim;
    public CreatureScript cs;
        
    // Start is called before the first frame update

  
    void Start()
    {
        NPC_panel.SetActive(false);
        buttonref.action.performed += Abutton;
        
    }

    // Update is called once per frame
   
    public void Interact()
    {
        if (NPC_talking == false)
        {
            StartTalking();
            NPC_panel.SetActive(true);
            NPC_dialogueindex = 0;
            NPC_talking = true;
            NPC_name_text.text = NPC_name;
            NextLine();
        }
        else if (NPC_talking == true)
        {
            NextLine();
        }
    }
    public void EndTalk()
    {
        NPC_panel.SetActive(false);
        NPC_talking = false;
        NPC_text.text = "";
        NPC_name_text.text = "";
        NPC_dialogueindex = 0;
        

    }
    public void NextLine()
    {
      

        if(NPC_dialogueoption == 1) 
        {
            if (NPC_dialogueindex < NPC_dialogue.Count)
            {
                NPC_text.text = NPC_dialogue[NPC_dialogueindex];
                NPC_dialogueindex++;
            }
            else
            {
            
                EndTalk(); 
            }
        }

       
        if (NPC_dialogueoption == 2)
        { 


             if (NPC_dialogueindex < NPC_dialogue2.Count)
            {
                NPC_text.text = NPC_dialogue2[NPC_dialogueindex];

                if (this.gameObject.CompareTag("icemoon") || this.gameObject.CompareTag("firemoon"))
                {
                    NPC_name_text.text = "Moon (Mirage)";
                    LevelMoon();
                }

               

                NPC_dialogueindex++;

               
            }
            else
            {
                 
                    NPC_dialogueoption = 3;
                    NPC_dialogueindex = 0;

                    NextLine();
                
            }
        }

        if (NPC_dialogueoption == 3)
        {
            if (NPC_dialogueindex < NPC_dialogue3.Count)
            {
                NPC_text.text = NPC_dialogue3[NPC_dialogueindex];
                
                
                    NPC_name_text.text = "Revived Moon Man";
                    MoonManLeaving();
               

                NPC_dialogueindex++;
            }
            else
            {
                EndTalk();
            }
        }


        if (NPC_dialogueoption == 4)
        {
            if (NPC_dialogueindex < NPC_dialogue4.Count)
            {
                NPC_text.text = NPC_dialogue4[NPC_dialogueindex];
                NPC_dialogueindex++;
            }
            else
            {
                EndTalk();
            }
        }

    }
    public void SetDialogueOption(int option)
    {
        NPC_dialogueoption = option;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            NPC_dialogueindex = 1;
            Interact();
            Debug.Log("twice?");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EndTalk();
        }
    }


    void Abutton(InputAction.CallbackContext context)
    {
      

        if (NPC_talking == true)
        {
            Interact();
            audio.PlayOneShot(clip, 0.7f);

            //voicestuff();
    
        }
    }


    public void LevelMoon()
    {
        moonanim.SetBool("Talking", true);


        if (NPC_dialogueindex == 0)
        {
            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 1)
        {

            facesoff();
            moonanim.SetBool("Sad", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 2)
        {

            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
            
        }

    }

    

    public void MoonManLeaving()
    {
        moonmananim.SetBool("Talking", true);
        if (NPC_dialogueindex >= 3)
        {
            Debug.Log("It should work but it don't");
            moonmananim.ResetTrigger("Walking");
            moonmananim.SetTrigger("Walking");
        }
    }

    public void StartTalking()
    {
        moonanim.SetBool("Talking", true);
      
    }

    public void talkend()
    {
        moonanim.SetBool("Talking", false);
       
    }

    public void facesoff()
    {
        moonanim.SetBool("Disappoint", false);
        moonanim.SetBool("Flustered", false);
        moonanim.SetBool("Sad", false);
        moonanim.SetBool("Happy", false);
    }
    public void voicestuff()
    {
        //var index = Random.Range(0, voicelist.Count);
        //audio.PlayOneShot(voicelist[index], 1f);
    }

}

