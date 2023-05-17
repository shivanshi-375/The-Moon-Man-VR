using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC_script : MonoBehaviour
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

    public bool rotated = false;
    public bool rotatedfirst = false;
   
    public bool diedonce = false;
    [SerializeField] private InputActionReference buttonref;
    [SerializeField] private InputActionReference turnref;

   

    public Animator moonanim;
    public bool talkingover = false;
    public bool talkingover2 = false;
    public bool endnow = false;
    public bool whaleout = false;
    public bool firstb = false;
    public bool firstw = false;
    public Animator letteranim;
    public GameObject letter;
    // Start is called before the first frame update
    public GameObject whale;
    public GameObject EndText;

    private GameManager game_manager;

    public string scenename;

    [Tooltip("Location Where the Player is placed when scenename gets loaded.")]
    public Transform location_when_loaded;
    void Start()
    {
        NPC_panel.SetActive(false);
        buttonref.action.performed += Abutton;
        turnref.action.performed += turning;
        talkingover = false;
        talkingover2 = false;
        endnow = false;
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
   
    public void Interact()
    {
        if (NPC_talking == false)
        {
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

        if(talkingover == true)
        {
            SceneManager.LoadScene("StrandedShore");
        }

        if(talkingover2 == true)
        {
            Debug.Log("right again");
            game_manager.LoadScene(scenename, location_when_loaded);
        }

        if(whaleout == true)
        {
            Debug.Log("whale");
            whale.SetActive(true);
            Invoke("whalefinally", 30f);
        }

        if(endnow == true)
        {
            TheEnd();
        }
    }
    public void NextLine()
    {
        if(NPC_dialogueoption == 1) 
        {
            if (NPC_dialogueindex < NPC_dialogue.Count)
            {

                NPC_text.text = NPC_dialogue[NPC_dialogueindex];

                if (this.gameObject.CompareTag("strandedmoon"))
                {
                    MoonTutorial();

                } else if (this.gameObject.CompareTag("MoonRefuge1"))
                {
                    
                    RefugeTalk();


                } else if (this.gameObject.CompareTag("MoonRefuge2"))
                {
                    Debug.Log("yup its right");
                    FinalRefuge();

                } else if(this.gameObject.CompareTag("MoonFinal"))
                {
                    Cinematic();
                }
                    
                else {
                    TitleMoon();
                    
                }

              

                

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

                if(this.gameObject.CompareTag("strandedmoon"))
                {
                    MoonTutorial2();
                } else if (this.gameObject.CompareTag("MoonFinal"))
                {
                    whaleconvo();
                }

                    NPC_dialogueindex++;


            }
            else
            {
                EndTalk();
            }
        }

        if (NPC_dialogueoption == 3)
        {
            if (firstw == true && NPC_dialogueindex == NPC_dialogue3.Count)
            {
                NPC_dialogueoption = 4;
                NPC_dialogueindex = 0;
                Interact();
            }
            else if (NPC_dialogueindex < NPC_dialogue3.Count)
            {

                NPC_text.text = NPC_dialogue3[NPC_dialogueindex];

                if (this.gameObject.CompareTag("strandedmoon"))
                {
                    MoonTutorial3();
                }

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

                if (this.gameObject.CompareTag("strandedmoon"))
                {
                    MoonTutorial4();
                }

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

    void turning(InputAction.CallbackContext context)
    {
        Debug.Log("turning now");
        rotated = true;
        if (NPC_talking == false)
        {
            if (rotated == true && rotatedfirst == false && NPC_dialogueindex == NPC_dialogue.Count)
            {
                if (!this.gameObject.CompareTag("MoonFinal"))
                {
                    NPC_dialogueoption = 2;
                    rotatedfirst = true;
                    NPC_dialogueindex = 0;
                    Interact();
                }
               
            }
            

        }
    }



    public void RefugeTalk()
    {
        if (this.gameObject.CompareTag("MoonRefuge1"))
        {
            if (NPC_dialogueindex == 0)
            {
                facesoff();
                moonanim.SetBool("Sad", true);
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
                moonanim.SetBool("Sad", true);
                moonanim.SetBool("Talking", true);
                Invoke("talkend", 2f);

            }

            if (NPC_dialogueindex == 3)
            {
                facesoff();
                moonanim.SetBool("Disappoint", true);
                moonanim.SetBool("Talking", true);
                Invoke("talkend", 2f);

            }

            if (NPC_dialogueindex == 4)
            {
                facesoff();
                moonanim.SetBool("Disappoint", true);
                moonanim.SetBool("Talking", true);
                Invoke("talkend", 2f);

            }

            if (NPC_dialogueindex == 5)
            {
                facesoff();
                moonanim.SetBool("Happy", true);
                moonanim.SetBool("Talking", true);
                Invoke("talkend", 2f);

            }

            if (NPC_dialogueindex == 6)
            {
                facesoff();
                moonanim.SetBool("Disappoint", true);
                moonanim.SetBool("Talking", true);
                Invoke("talkend", 2f);

            }

            if (NPC_dialogueindex == 7)
            {
                facesoff();
                moonanim.SetBool("Flustered", true);
                moonanim.SetBool("Talking", true);
                Invoke("talkend", 2f);

            }
        }

    }

    public void FinalRefuge()
    {
        if (NPC_dialogueindex == 0)
        {
            facesoff();
            moonanim.SetBool("Sad", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 1)
        {
            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
            talkingover2 = true;
        }
    }

    public void Cinematic()
    {
        if (NPC_dialogueindex == 0)
        {
            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 1)
        {
            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
            
        }

        if (NPC_dialogueindex == 2)
        {
            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
            
            whaleout = true;
        }
    }

    public void MoonTutorial()
    {
        if (this.gameObject.CompareTag("strandedmoon"))
        {
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
                moonanim.SetBool("Disappoint", true);
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

            if (NPC_dialogueindex == 3)
            {
                facesoff();
                moonanim.SetBool("Happy", true);
                moonanim.SetBool("Talking", true);
                Invoke("talkend", 2f);

            }
        }
    }

    public void MoonTutorial2()
    {
        if (NPC_dialogueindex == 1)
        {
            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 2)
        {
            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }
    }

    public void MoonTutorial3()
    {
        if (NPC_dialogueindex == 0)
        {
            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 1)
        {
            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 2)
        {
            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 3)
        {
            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 4)
        {
            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 5)
        {
            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 6)
        {
            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }
    }

    public void MoonTutorial4()
    {
        if (NPC_dialogueindex == 0)
        {
            facesoff();
            moonanim.SetBool("Sad", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 1)
        {
            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 2)
        {
            Debug.Log("descending");
            letteranim.ResetTrigger("Descend");
            letteranim.SetTrigger("Descend");
            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 3)
        {
            
            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
            letter.SetActive(true);
            
        }

      
    }

    public void TitleMoon()
    {
        moonanim.SetBool("Talking", true);
       

        if (NPC_dialogueindex == 1)
        {
            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 2)
        {

            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 3)
        {

            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 4)
        {

            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 5)
        {

            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 6)
        {

            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 7)
        {

            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 8)
        {

            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 9)
        {

            facesoff();
            moonanim.SetBool("Sad", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 10)
        {

            facesoff();
            moonanim.SetBool("Sad", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 11)
        {

            facesoff();
            moonanim.SetBool("Sad", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 12)
        {

            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 13)
        {

            facesoff();
            Invoke("happyswitch", 0.5f);
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

        if (NPC_dialogueindex == 14)
        {

            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
        }

       

        if (NPC_dialogueindex == 15)
        {

            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
            talkingover = true;
        }

       
    }

    public void firstdrown()
    {
        
        if(firstb == false && NPC_dialogueindex == NPC_dialogue2.Count)
            {
            firstb = true;
            NPC_dialogueoption = 3;
            NPC_dialogueindex = 0;
            Interact();
            }
    }

    public void firstwater()
    {

        if (firstw == true && NPC_dialogueindex == NPC_dialogue3.Count)
        {
            NPC_dialogueoption = 4;
            NPC_dialogueindex = 0;
            Interact();
        }
    }
    public void happyswitch()
    {
        moonanim.SetBool("Happy", false);
        moonanim.SetBool("Flustered", true);
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

    public void whaleconvo()
    {
        if (NPC_dialogueindex == 0)
        {
            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 1)
        {
            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 2)
        {
            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
            letter.SetActive(true);

        }

        if (NPC_dialogueindex == 3)
        {
            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 4)
        {
            facesoff();
            moonanim.SetBool("Sad", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 5)
        {
            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 6)
        {
            facesoff();
            moonanim.SetBool("Flustered", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 7)
        {
            facesoff();
            moonanim.SetBool("Disappoint", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);

        }

        if (NPC_dialogueindex == 8)
        {
            facesoff();
            moonanim.SetBool("Happy", true);
            moonanim.SetBool("Talking", true);
            Invoke("talkend", 2f);
            endnow = true;
        }
    }

    public void whalefinally()
    {
        NPC_dialogueoption = 2;
        NPC_dialogueindex = 0;
        Interact();
    }

    public void TheEnd()
    {
        EndText.SetActive(true);
        
    }
    public void voicestuff()
    {
        //var index = Random.Range(0, voicelist.Count);
        //audio.PlayOneShot(voicelist[index], 1f);
    }

}

