using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSmashingManager : BaseItem
{
	public AudioSource sound;
    public GameObject smashedObj;

    private void FixedUpdate()
    {
        if (!itemPicked && itemThrown && GetComponent<Rigidbody>().velocity.magnitude <= 0.000001f)
        {
			Debug.Log("Close trow");
						//itemThrown = false; //Item has settled
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
		Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
		if (itemThrown)
        {
			Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
            if (GetComponent<Rigidbody>().velocity.magnitude > 3.5f)
            {
                SmashItemMesh(true);
            }

            if (collision.collider.GetComponent<ObjectSmashingManager>())
            {
                collision.collider.GetComponent<ObjectSmashingManager>().SmashItemMesh(true);
            }
			itemThrown = false;

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
                item.GetComponent<Rigidbody>().AddForce(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f), ForceMode.Impulse);
            }
        }
				if (sound) sound.Play();

				Destroy(gameObject);
    }
}
