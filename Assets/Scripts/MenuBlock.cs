using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBlock : MonoBehaviour
{

    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHand") && this.gameObject.CompareTag("RestartBlock"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (other.gameObject.CompareTag("PlayerHand") && this.gameObject.CompareTag("QuitBlock"))
        {
            Application.Quit();
        }

        if (other.gameObject.CompareTag("PlayerHand") && this.gameObject.CompareTag("SettingsBlock"))
        {
            canvas.SetActive(true);
        }

    }
}
