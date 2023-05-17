using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public string scenename;

    [Tooltip("Location Where the Player is placed when scenename gets loaded.")]
    public Transform location_when_loaded;


    private GameManager game_manager;
    // Start is called before the first frame update
    void Start()
    {
        game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            game_manager.LoadScene(scenename, location_when_loaded);
        }
    }
}
