using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightReflector : MonoBehaviour
{
    public bool is_in_moonlight = false;
    public int max_bounces = 100;//This is here because it is possible to run into situations where the reflections would never stop.


    private const float NO_HIT_LINE_LENGTH = 2000.0f;

    private List<Vector3> hit_positions;
    private LineRenderer lr;


    private PlantScript disabled_plant;
    private CreatureScript creature;
    bool is_disabling_plant = false;

    // Start is called before the first frame update
    void Start()
    {
        hit_positions = new List<Vector3>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
    }

    void FixedUpdate()
    {
        hit_positions.Clear();
        RaycastHit raycast_result;
        bool it_hit = Physics.Raycast(transform.position, transform.forward, out raycast_result);

        is_disabling_plant = false;

        lr.positionCount = 2;
        hit_positions.Add(transform.position);

        if (it_hit)
        {
            Vector3 new_position = raycast_result.point + raycast_result.normal * 0.000001f;
            hit_positions.Add(new_position);

            if (raycast_result.collider.tag == "LightReflector")
            {
                Vector3 new_direction = Vector3.Reflect(transform.forward, raycast_result.normal);
                do_recursive_ray(new_position, new_direction);
            }
            else if (raycast_result.collider.GetComponent<PlantScript>())
            {
                PlantScript plant = raycast_result.collider.GetComponent<PlantScript>();
                disabled_plant = plant;
                is_disabling_plant = true;
            } else if (raycast_result.collider.GetComponent<CreatureScript>())
            {
                CreatureScript creat = raycast_result.collider.GetComponent<CreatureScript>();
                creature = creat;
                is_disabling_plant = true;
            } else
            {
                is_disabling_plant = false;
            }

        } else {
            //lr.positionCount = 0;
            hit_positions.Add(transform.position + transform.forward * NO_HIT_LINE_LENGTH);
            is_disabling_plant = false;
        }


        for(int i = 0; i < hit_positions.Count; i++)
        {
            lr.SetPosition(i, hit_positions[i]);
        }

        if (is_disabling_plant)
        {
            if (disabled_plant != null) {
                disabled_plant.shrink();
            }
            
            else if (creature != null)
            {
                creature.shrink();
            }
            
        } else if(disabled_plant)
        {
            Debug.Log("Regrow!");
            disabled_plant.grow();
            
            disabled_plant = null;
        } else if (creature)
        {
            Debug.Log("Creature is saved");
            creature.grow();
            creature = null;
        }
    }


    void do_recursive_ray(Vector3 position, Vector3 direction)
    {
        if (hit_positions.Count >= max_bounces) return;

        RaycastHit raycast_result;
        bool it_hit = Physics.Raycast(position, direction, out raycast_result);

        lr.positionCount += 1;
        if (it_hit)
        {
            Vector3 new_position = raycast_result.point + raycast_result.normal * 0.000001f;
            hit_positions.Add(new_position);


            if (raycast_result.collider.tag == "LightReflector")
            {
                Vector3 new_direction = Vector3.Reflect(direction, raycast_result.normal);
                do_recursive_ray(new_position, new_direction);
            } else if (raycast_result.collider.GetComponent<PlantScript>())
            {
                PlantScript plant = raycast_result.collider.GetComponent<PlantScript>();
                disabled_plant = plant;
                is_disabling_plant = true;
            } else
            {
                is_disabling_plant = false;
            }
        } else
        {
            is_disabling_plant = false;
            hit_positions.Add(transform.position + transform.forward * NO_HIT_LINE_LENGTH);
            is_disabling_plant = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Moonlitspot")
        {
            is_in_moonlight = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Moonlitspot")
        {
            is_in_moonlight = false;
        }
    }
}
