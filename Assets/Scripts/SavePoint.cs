using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{

    public Transform respawn;
    public PlayerController2 script;
    public AudioSource audio;
    public AudioClip clip;
    public Animator anim;
    public bool IamSavePoint = false;

    public SavePoint save1;
    public SavePoint save2;
    public SavePoint save3;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        checkothersaves();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("yay");
        if (other.gameObject.CompareTag("Player"))
        {
            IamSavePoint = true;
            anim.SetBool("Bloom", true);
            script.Respawn.position = respawn.position;
            audio.PlayOneShot(clip, 0.7F);

            Invoke("falsify", 3f);
        }
        
    }

    public void checkothersaves()
    {
        if(save1.IamSavePoint == true || save2.IamSavePoint == true || save3.IamSavePoint == true)
        {
            
            anim.SetBool("Bloom", false);
        }
    }

    public void falsify()
    {
        IamSavePoint = false;
    }
 
}
