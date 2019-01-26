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

<<<<<<< HEAD
    protected void SmashItemMesh(bool addExplosion)
=======
    public void SmashItemMesh(bool addExplosion)
>>>>>>> 9f14a6be653bbaa04204909672dea7094c4df765
    {

    }
}
