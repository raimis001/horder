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
			//Debug.Log("Close trow");
			//itemThrown = false; //Item has settled
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (itemThrown)
		{
			if (GetComponent<Rigidbody>().velocity.magnitude > 2.5f)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;

				SmashItemMesh(true);
			}

			if (collision.collider.GetComponent<ObjectSmashingManager>())
			{
				collision.collider.GetComponent<ObjectSmashingManager>().SmashItemMesh(true);
			}
			itemThrown = false;
<<<<<<< HEAD
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
=======

		}
	}

	public void SmashItemMesh(bool addExplosion)
	{
		Debug.Log("Smashing");
		if (!smashedObj) return;

		GameObject t = Instantiate(smashedObj, transform.position, transform.rotation);
		if (addExplosion)
		{
			foreach (Transform item in t.transform)
			{
				item.GetComponent<Rigidbody>().AddForce(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f), ForceMode.Impulse);
				//item.GetComponent<Rigidbody>().isKinematic = true;
			}
		}
		if (sound) sound.Play();

		Destroy(gameObject);
	}
>>>>>>> b58b24e0fa70824abe82942b0790e865f36696fc
}
