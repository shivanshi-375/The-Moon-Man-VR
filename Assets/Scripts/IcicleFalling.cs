using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleFalling : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Vector3 pos;
    public float timeout = 5f;
    public float randomTime; 
    private bool isFalling = false;
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
        pos = GetComponent<Rigidbody>().position;
        rigidbody.useGravity = false;
        StartCoroutine(SpawnIcicles());
    }
    IEnumerator SpawnIcicles()
    {
        randomTime = Random.Range(2f, 10f);
        rigidbody = GetComponent<Rigidbody>();
        yield return new WaitForSeconds(randomTime);
        rigidbody.useGravity = true;
        // StartCoroutine(destroy());
        isFalling = true;
    }

    // IEnumerator destroy()
    // {
    //     yield return new WaitForSeconds(1f);
    //     isFalling = true;
    //     yield return new WaitForSeconds(timeout);
    //     Destroy(rigidbody.gameObject);
    // }
    private void OnCollisionEnter(Collision other) {
        
        if (isFalling) 
        {
            Instantiate(rigidbody, pos, Quaternion.Euler(90, 0, 30));
            Destroy(rigidbody.gameObject);
        }

        if (other.gameObject.CompareTag("IcePlatform"))
        {
            Debug.Log("Icicle hit");
            Destroy(other.gameObject);
        }
    }
}
