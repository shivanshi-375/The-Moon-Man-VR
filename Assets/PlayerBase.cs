using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.XR.Interaction.Toolkit;

using static GameManager;

public class PlayerBase : MonoBehaviour
{
    public GameObject moonmanice;
    public GameObject moonmanfire;
    public GameObject icepouches;
    public GameObject firepouches;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (SceneManager.GetActiveScene().name == "LunarRefuge1")
            findmoonmen();

        if (gm == null || !gm.latest_spawn_location_is_valid)
            return;

        transform.SetPositionAndRotation(gm.latest_spawn_location.position, gm.latest_spawn_location.rotation);

        MyPlayerSettings settings = gm.settings;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Debug.Log("Start Loading settings");
            player.GetComponent<Rigidbody>().drag = settings.rb_drag;
            player.GetComponent<ActionBasedContinuousMoveProvider>().moveSpeed = settings.move_speed;


            TunnelingVignetteController vignette_controller = (TunnelingVignetteController)player.GetComponentsInChildren(typeof(TunnelingVignetteController), true)[0];

            if (settings.vignette_enabled)
            {
                vignette_controller.enabled = true;
                vignette_controller.gameObject.SetActive(true);
                Debug.Log("Have enabled!");
            }
            else
            {
                vignette_controller.enabled = false;
                vignette_controller.gameObject.SetActive(false);
                Debug.Log("Have Disabled!");
            }

            ActionBasedSnapTurnProvider turn_provider = player.GetComponent<ActionBasedSnapTurnProvider>();
            turn_provider.turnAmount = settings.turn_amount;

            ActionBasedContinuousTurnProvider tp = player.GetComponent<ActionBasedContinuousTurnProvider>();
            tp.turnSpeed = settings.turn_speed;

            turn_provider.enabled = settings.snap_turning_enabled;
            tp.enabled = !settings.snap_turning_enabled;

            Debug.Log("Finish Loading settings!");
        }


        if (SceneManager.GetActiveScene().name == "LunarRefuge1")
            opensesame(gm);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void findmoonmen()
    {
        moonmanfire = GameObject.Find("MoonManObsidian2");
        moonmanice = GameObject.Find("MoonManObsidian");
        firepouches = GameObject.Find("BeatLevel1");
        icepouches = GameObject.Find("BeatLevel");
        moonmanfire.SetActive(false);
        moonmanice.SetActive(false);
        icepouches.SetActive(false);
        firepouches.SetActive(false);
        Debug.Log("we found them");
    }

    public void opensesame(GameManager gm)
    {

        if (gm.has_completed_ice_level == true && gm.has_completed_fire_level == true)
        {
            moonmanfire.SetActive(true);
            moonmanice.SetActive(true);
            icepouches.SetActive(true);
            firepouches.SetActive(true);
        }

    }
}