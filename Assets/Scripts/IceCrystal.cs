using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCrystal : MonoBehaviour
{
    public GameObject TearOfSorrow;
    public Transform insidepouch;
    public int inventory;
    
    public IceCrystal script;
    public GameObject NewIceCrystal;

    public GameObject iceplat_prefab;
    public GameObject water;
    public bool emptypouch = false;

    public AudioSource audio;
    public AudioClip clip;
    public bool pickedup = false;

    public float offsety = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        inventory = 4;
        pickedup = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 get_water_surface_position(Vector3 pos)
    {
        RaycastHit hit;

        LayerMask mask = LayerMask.GetMask("Wall");
        Vector3 origin = pos + Vector3.up;
        if (Physics.Raycast(pos, Vector3.down, out hit, Mathf.Infinity, mask))
            return hit.point;
        else
            return pos;//Just fallback to whatever we pass in if we can't find any water.
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Water") && pickedup == true)
        {
            pickedup = false;
            water = other.gameObject;
            Debug.Log("Ice Platform!");

            Vector3 platform_pos = get_water_surface_position(transform.position);

            platform_pos.y -= offsety;

            Debug.Log(transform.position);

            GameObject iceplatform = Instantiate(iceplat_prefab, platform_pos, Quaternion.identity);
            iceplatform.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            iceplatform.transform.SetParent(water.transform, true);
            Collider platform_collider = iceplatform.GetComponent<BoxCollider>();
            platform_collider.enabled = true;
            iceplatform.transform.position = platform_pos;

            UnityEngine.Debug.Log(platform_pos);

            NewIceCrystal = Instantiate(TearOfSorrow, insidepouch.position, Quaternion.identity);
            script = NewIceCrystal.transform.gameObject.GetComponent<IceCrystal>();
            script.pickedup = false;

            Destroy(gameObject);
        }
    }
    public void returntoinventory()
    {
        inventory++;
    }

    public void aftergrab()
    {
        if(inventory > 0 && emptypouch == true)
        {
            inventory--;
            emptypouch = false;
            NewIceCrystal = Instantiate(TearOfSorrow, insidepouch.position, Quaternion.identity);
            script = NewIceCrystal.transform.gameObject.GetComponent<IceCrystal>();
            script.inventory = inventory--;
            script.emptypouch = false;
            
        }
        
    }

    public void pickup()
    {
        audio.PlayOneShot(clip);
        pickedup = true;
    }
    public void empty()
    {
        emptypouch = true;
    }

    
}
