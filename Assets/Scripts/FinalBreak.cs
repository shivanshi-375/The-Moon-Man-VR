using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalBreak : MonoBehaviour
{

    public GameObject spotlight;
    public GameObject wall;
    public TMP_Text text;
    public TMP_Text tips;
    public Animator TipAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        spotlight.SetActive(true);
        tips.text = "You Win!";
        TipAnim.Play("Base Layer.TipCanvas", 0, 0);
        wall.SetActive(false);
    }
}
