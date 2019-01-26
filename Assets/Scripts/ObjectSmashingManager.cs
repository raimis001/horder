using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSmashingManager : BaseItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude <= 0.1f)
        {
            itemThrown = false;
            //Debug.Log("item has settled");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (itemThrown)
        {
            if (GetComponent<Rigidbody>().velocity.magnitude > 5f)
            {
                Debug.Log("item Really Smashed");
                return;
            }
            if (GetComponent<Rigidbody>().velocity.magnitude > 3.5f)
            {
                Debug.Log("item smashed");
            }
        }
    }

    private void SmashItemMesh(bool addExplosion)
    {

    }
}
