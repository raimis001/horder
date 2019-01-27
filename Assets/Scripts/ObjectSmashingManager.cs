using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSmashingManager : BaseItem
{
    public GameObject smashedObj;

    private void FixedUpdate()
    {
        if (itemThrown && GetComponent<Rigidbody>().velocity.magnitude <= 0.1f)
        {
            itemThrown = false; //Item has settled
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (itemThrown)
        {
            if (GetComponent<Rigidbody>().velocity.magnitude > 3.5f)
            {
                SmashItemMesh(true);
            }

            if (collision.collider.GetComponent<ObjectSmashingManager>())
            {
                collision.collider.GetComponent<ObjectSmashingManager>().SmashItemMesh(true);
            }
        }
    }

    public void SmashItemMesh(bool addExplosion)
    {
        Debug.Log("Smashing");
        if (!smashedObj) return;

        Instantiate(smashedObj, transform.position, transform.rotation);
        if (addExplosion)
        {
            foreach (Transform item in smashedObj.transform)
            {
                item.GetComponent<Rigidbody>().AddForce(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f), ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}
