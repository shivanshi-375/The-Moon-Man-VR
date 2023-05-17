using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.XR.Interaction.Toolkit;

public class MyPlayerSettings
{
    public float rb_drag;
    public float move_speed;
    public float turn_amount;
    public float turn_speed;
    public bool snap_turning_enabled;
    public bool vignette_enabled;
}


public class GameManager : MonoBehaviour
{
    public MyPlayerSettings settings;

    public bool has_completed_ice_level = false;
    public bool has_completed_fire_level = false;

    static GameManager singleton = null;

    public Transform latest_spawn_location = null;
    public bool latest_spawn_location_is_valid = false;

   

    void Awake()
    {
        if (GameManager.singleton == null)
        {
            GameManager.singleton = this;
        } else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        

    }

    // Start is called before the first frame update
    void Start()
    {

       

        DontDestroyOnLoad(this.gameObject);

        settings = new MyPlayerSettings();
        

    }

    public void complete_fire_level()
    {
        if (!has_completed_fire_level)
        {
            has_completed_fire_level = true;
        }
    }

    public void complete_ice_level()
    {
        if (!has_completed_ice_level)
        {
            has_completed_ice_level = true;
        }
    }

    public void LoadScene(string scene_name, Transform player_spawn)
    {
        SceneManager.LoadScene(scene_name);

        latest_spawn_location = player_spawn;
        latest_spawn_location_is_valid = true;
        //opensesame();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Debug.Log("PLATER DNFKDFLJ");

            ActionBasedContinuousMoveProvider continuous_move_provider = player.GetComponent<ActionBasedContinuousMoveProvider>();

            settings.move_speed = continuous_move_provider.moveSpeed;


            settings.rb_drag = player.GetComponent<Rigidbody>().drag;

            TunnelingVignetteController vignette_controller = (TunnelingVignetteController)player.GetComponentsInChildren(typeof(TunnelingVignetteController), true)[0];
            settings.vignette_enabled = vignette_controller.gameObject.activeInHierarchy;

            if (settings.vignette_enabled)
                Debug.Log("Want Enabled!");
            else
                Debug.Log("Want Disabled!");
            ActionBasedSnapTurnProvider turn_provider = player.GetComponent<ActionBasedSnapTurnProvider>();
            settings.turn_amount = turn_provider.turnAmount;

            settings.snap_turning_enabled = turn_provider.enabled;

            ActionBasedContinuousTurnProvider tp = player.GetComponent<ActionBasedContinuousTurnProvider>();
            settings.turn_speed = tp.turnSpeed;

            Debug.Log("Done saving settings.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



  
}